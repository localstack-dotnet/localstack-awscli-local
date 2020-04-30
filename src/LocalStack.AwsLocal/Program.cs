using LocalStack.Client;
using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics;
using System.Linq;

namespace LocalStack.AwsLocal
{
    internal static class Program
    {
        private static readonly string LocalStackHost = Environment.GetEnvironmentVariable("LOCALSTACK_HOST");

        private static void Main(string[] args)
        {
            var rootCommand = new RootCommand();
            ParseResult parseResult = rootCommand.Parse(args);
            var parsedArgs = parseResult.Tokens.Select(token => token.Value).ToArray();

            var processHelper = new ProcessHelper();
            var config = new Config(LocalStackHost);

            var commandDispatcher = new CommandDispatcher(processHelper, config, parsedArgs);

            commandDispatcher.Run();
        }
    }
}