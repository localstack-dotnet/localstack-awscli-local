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
            var textWriter = Console.Out;

            var commandDispatcher = new CommandDispatcher(processHelper, config, textWriter, args);

            commandDispatcher.Run();
        }
    }
}