using LocalStack.Client;
using System;
using LocalStack.AwsLocal.ProcessCore;
using LocalStack.AwsLocal.ProcessCore.IO;
using LocalStack.Client.Models;
using LocalStack.Client.Options;

namespace LocalStack.AwsLocal
{
    internal static class Program
    {
        private static readonly string? LocalStackHost = Environment.GetEnvironmentVariable("LOCALSTACK_HOST");
        private static readonly string? UseSsl = Environment.GetEnvironmentVariable("USE_SSL");
        private static readonly string? UseLegacyPorts = Environment.GetEnvironmentVariable("USE_LEGACY_PORTS");
        private static readonly string? UseEdgePort = Environment.GetEnvironmentVariable("EDGE_PORT");

        private static void Main(string[] args)
        {
            //Console.WriteLine("Waiting for debugger to attach");
            //while (!Debugger.IsAttached)
            //{
            //    Thread.Sleep(100);
            //}
            //Console.WriteLine("Debugger attached");

            string localStackHost = !string.IsNullOrEmpty(LocalStackHost) ? LocalStackHost : Constants.LocalStackHost;
            bool useSsl = !string.IsNullOrEmpty(UseSsl) && (UseSsl == "1" || UseSsl == "true");
            bool useLegacyPorts = !string.IsNullOrEmpty(UseLegacyPorts) || (UseLegacyPorts == "1" || UseLegacyPorts == "true");
            int edgePort = int.TryParse(UseEdgePort, out int port) ? port : Constants.EdgePort;

            var configOptions = new ConfigOptions(localStackHost, useSsl, useLegacyPorts, edgePort);

            var processRunner = new ProcessRunner();
            var config = new Config(configOptions);
            var fileSystem = new FileSystem();

            var commandDispatcher = new CommandDispatcher(processRunner, config, fileSystem, args);

            commandDispatcher.Run();
        }
    }
}