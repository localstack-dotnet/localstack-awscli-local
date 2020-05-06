using LocalStack.Client;
using System;
using System.Diagnostics;
using System.Threading;
using LocalStack.AwsLocal.ProcessCore;
using LocalStack.AwsLocal.ProcessCore.IO;

namespace LocalStack.AwsLocal
{
    internal static class Program
    {
        private static readonly string LocalStackHost = Environment.GetEnvironmentVariable("LOCALSTACK_HOST");

        private static void Main(string[] args)
        {
            //Console.WriteLine("Waiting for debugger to attach");
            //while (!Debugger.IsAttached)
            //{
            //    Thread.Sleep(100);
            //}
            //Console.WriteLine("Debugger attached");

            var processRunner = new ProcessRunner();
            var config = new Config(LocalStackHost);
            var fileSystem = new FileSystem();

            var commandDispatcher = new CommandDispatcher(processRunner, config, fileSystem, args);

            commandDispatcher.Run();
        }
    }
}