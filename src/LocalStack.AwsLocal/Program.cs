using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LocalStack.AwsLocal.Contracts;
using LocalStack.Client;
using LocalStack.Client.Models;

namespace LocalStack.AwsLocal
{
    public class Program
    {
        private const string UsageResource = "LocalStack.AwsLocal.Usage.txt";
        private static  readonly IProcessHelper ProcessHelper = new ProcessHelper();

        private static string[] _args;
        private static IEnumerable<string> Args => _args;

        static void Main(string[] args)
        {
            _args = args;

            if (args.Length == 0 || (args[0] == "-h"))
            {
                Usage();
            }

            string localStackHost = Environment.GetEnvironmentVariable("LOCALSTACK_HOST");

            var awsServiceEndpoint = GetServiceEndpoint(localStackHost);
            string service = GetService();

            if (awsServiceEndpoint == null || service == null)
            {
                Console.WriteLine($"ERROR: Unable to find LocalStack endpoint for service {service}");
                Environment.Exit(1);
            }

            var arguments = args.ToList();
            arguments.Insert(0, "aws");
            arguments.Insert(1, $"--endpoint-url={awsServiceEndpoint.ServiceUrl}");

            if (awsServiceEndpoint.Host.Contains("https"))
            {
                arguments.Insert(2, "--no-verify-ssl");
            }

            string awsDefaultRegion = Environment.GetEnvironmentVariable("AWS_DEFAULT_REGION") ?? "us-east-1";
            string awsAccessKeyId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") ?? "_not_needed_locally_";
            string awsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") ?? "_not_needed_locally_";

            ProcessHelper.CmdExecute(string.Join(' ', arguments), null, true, true, new Dictionary<string, string>
            {
                {"AWS_DEFAULT_REGION", awsDefaultRegion},
                { "AWS_ACCESS_KEY_ID", awsAccessKeyId},
                {"AWS_SECRET_ACCESS_KEY", awsSecretAccessKey}
            });
        }

        private static string GetService()
        {
            foreach (string arg in Args)
            {
                if (!arg.StartsWith('-'))
                {
                    return arg;
                }
            }

            return string.Empty;
        }

        private static AwsServiceEndpoint GetServiceEndpoint(string localStackHost = null)
        {
            string service = GetService();
            if (service == "s3api")
            {
                service = "s3";
            }

            var awsServiceEndpoints = Config.GetAwsServiceEndpoints(localStackHost).ToList();
            return awsServiceEndpoints.SingleOrDefault(endpoint => endpoint.CliName == service);
        }


        private static void Usage()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(UsageResource))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }

            Environment.Exit(0);
        }
    }
}
