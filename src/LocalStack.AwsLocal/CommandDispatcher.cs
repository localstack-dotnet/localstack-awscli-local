using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LocalStack.AwsLocal.AmbientContexts;
using LocalStack.AwsLocal.Contracts;
using LocalStack.AwsLocal.Extensions;
using LocalStack.Client.Contracts;
using LocalStack.Client.Models;

namespace LocalStack.AwsLocal
{
    public class CommandDispatcher
    {
        private const string UsageResource = "LocalStack.AwsLocal.Docs.Usage.txt";

        private readonly IProcessHelper _processHelper;
        private readonly IConfig _config;
        private readonly string[] _args;

        private CommandDispatcher()
        {
        }

        public CommandDispatcher(IProcessHelper processHelper, IConfig config, string[] args)
        {
            _processHelper = processHelper;
            _config = config;
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

            string cliCommand = _args.GetCliCommand(awsServiceEndpoint.ServiceUrl);

            string awsDefaultRegion = Environment.GetEnvironmentVariable("AWS_DEFAULT_REGION") ?? "us-east-1";
            string awsAccessKeyId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") ?? "_not_needed_locally_";
            string awsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") ?? "_not_needed_locally_";

            _processHelper.CmdExecute(cliCommand, null, true, true, new Dictionary<string, string>
            {
                {"AWS_DEFAULT_REGION", awsDefaultRegion},
                {"AWS_ACCESS_KEY_ID", awsAccessKeyId},
                {"AWS_SECRET_ACCESS_KEY", awsSecretAccessKey}
            });
        }

        private static string GetUsageInfo()
        {
            using (Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream(UsageResource))
            {
                using (var reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();

                    return result;
                }
            }
        }
    }
}
