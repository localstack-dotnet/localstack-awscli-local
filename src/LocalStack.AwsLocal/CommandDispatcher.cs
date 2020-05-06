using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using LocalStack.AwsLocal.AmbientContexts;
using LocalStack.AwsLocal.Contracts;
using LocalStack.AwsLocal.Extensions;
using LocalStack.AwsLocal.ProcessCore;
using LocalStack.AwsLocal.ProcessCore.IO;
using LocalStack.Client.Contracts;
using LocalStack.Client.Models;

namespace LocalStack.AwsLocal
{
    public class CommandDispatcher
    {
        private const string UsageResource = "LocalStack.AwsLocal.Docs.Usage.txt";

        private readonly IProcessRunner _processRunner;
        private readonly IConfig _config;
        private readonly IFileSystem _fileSystem;
        private readonly string[] _args;

        public CommandDispatcher(IProcessRunner processRunner, IConfig config, IFileSystem fileSystem, string[] args)
        {
            _processRunner = processRunner;
            _config = config;
            _fileSystem = fileSystem;
            _args = args;
        }

        public void Run()
        {
            if (_args.Length == 0 || (_args[0] == "-h"))
            {
                string usageInfo = GetUsageInfo();
                ConsoleContext.Current.WriteLine(usageInfo);
                EnvironmentContext.Current.Exit(0);
                return;
            }

            string serviceName = _args.ExtractServiceName();

            if (string.IsNullOrEmpty(serviceName))
            {
                ConsoleContext.Current.WriteLine("ERROR: Invalid argument, please enter a valid aws cli command");
                EnvironmentContext.Current.Exit(1);
                return;
            }

            AwsServiceEndpoint awsServiceEndpoint = _config.GetServiceEndpoint(serviceName);

            if (awsServiceEndpoint == null)
            {
                ConsoleContext.Current.WriteLine($"ERROR: Unable to find LocalStack endpoint for service {serviceName}");
                EnvironmentContext.Current.Exit(1);
                return;
            }

            var processSettings = new ProcessSettings
            { NoWorkingDirectory = true, Silent = true, EnvironmentVariables = new Dictionary<string, string>() };
            processSettings.EnvironmentVariables.Add("AWS_DEFAULT_REGION", Environment.GetEnvironmentVariable("AWS_DEFAULT_REGION") ?? "us-east-1");
            processSettings.EnvironmentVariables.Add("AWS_ACCESS_KEY_ID", Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") ?? "_not_needed_locally_");
            processSettings.EnvironmentVariables.Add("AWS_SECRET_ACCESS_KEY", Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") ?? "_not_needed_locally_");


            var builder = new ProcessArgumentBuilder();
            builder.Append(_args[0]);
            builder.AppendSwitch("--endpoint-url", "=", awsServiceEndpoint.ServiceUrl);

            if (awsServiceEndpoint.ServiceUrl.StartsWith("https"))
            {
                builder.Append("--no-verify-ssl");
            }

            var passToNextArgument = false;
            for (var i = 0; i < _args.Length; i++)
            {
                string argument = _args[i];
                
                if (argument == _args[0])
                {
                    continue;
                }

                if (passToNextArgument)
                {
                    passToNextArgument = false;
                    continue;
                }


                if (argument.StartsWith("--") && !argument.Contains("=") && i + 1 < _args.Length) // switch argument
                {
                    string nextArgument = _args[i + 1];
                    builder.AppendSwitchQuoted(argument, " ", nextArgument);

                    passToNextArgument = true;
                    continue;
                }

                builder.Append(argument);
            }


            processSettings.Arguments = builder;

            string awsExec = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "aws.cmd" : "aws";
            FilePath? awsPath = GetAwsPath(awsExec);

            if (awsPath == null)
            {
                ConsoleContext.Current.WriteLine($"ERROR: Unable to find aws cli");
                EnvironmentContext.Current.Exit(1);
                return;
            }

            IProcess? process = _processRunner.Start(awsPath, processSettings);

            process?.WaitForExit();
        }

        private static string GetUsageInfo()
        {
            using Stream? stream = Assembly.GetCallingAssembly().GetManifestResourceStream(UsageResource);
                using var reader = new StreamReader(stream ?? throw new NullReferenceException(nameof(stream)));
                    string result = reader.ReadToEnd();

            return result;
        }

        private FilePath? GetAwsPath(params string[] awsPaths)
        {
            string[]? pathDirs = null;

            // Look for each possible executable name in various places.
            foreach (string toolExeName in awsPaths)
            {

                // Cache the PATH directory list if we didn't already.
                if (pathDirs == null)
                {
                    string? pathEnv = Environment.GetEnvironmentVariable("PATH");
                    if (!string.IsNullOrEmpty(pathEnv))
                    {
                        pathDirs = pathEnv.Split(new[] { !RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ':' : ';' },
                            StringSplitOptions.RemoveEmptyEntries);
                    }
                    else
                    {
                        pathDirs = new string[] { };
                    }
                }

                // Look in every PATH directory for the file.
                foreach (var pathDir in pathDirs)
                {
                    FilePath file = new DirectoryPath(pathDir).CombineWithFilePath(toolExeName);
                    if (_fileSystem.Exist(file))
                    {
                        return file.FullPath;
                    }
                }
            }

            return null;
        }
    }
}
