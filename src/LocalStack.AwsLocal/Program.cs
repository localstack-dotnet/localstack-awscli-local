using LocalStack.Client;
using System;
using LocalStack.AwsLocal.ProcessCore;
using LocalStack.AwsLocal.ProcessCore.IO;

namespace LocalStack.AwsLocal
{
    internal static class Program
    {
        private static readonly string LocalStackHost = Environment.GetEnvironmentVariable("LOCALSTACK_HOST");

        private static void Main(string[] args)
        {
            var processRunner = new ProcessRunner();
            var config = new Config(LocalStackHost);
            var fileSystem = new FileSystem();

            var commandDispatcher = new CommandDispatcher(processRunner, config, fileSystem, args);

            commandDispatcher.Run();
        }
    }
}