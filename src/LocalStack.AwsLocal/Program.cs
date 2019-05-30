using LocalStack.Client;
using System;

namespace LocalStack.AwsLocal
{
    internal static class Program
    {
        private static readonly string LocalStackHost = Environment.GetEnvironmentVariable("LOCALSTACK_HOST");

        private static void Main(string[] args)
        {
            var processHelper = new ProcessHelper();
            var config = new Config(LocalStackHost);

            var commandDispatcher = new CommandDispatcher(processHelper, config, args);

            commandDispatcher.Run();
        }
    }
}