namespace LocalStack.AwsLocal;

internal static class Program
{
    private static readonly string LocalStackHost = Environment.GetEnvironmentVariable("LOCALSTACK_HOST");
    private static readonly string UseSsl = Environment.GetEnvironmentVariable("USE_SSL");
    private static readonly string UseLegacyPorts = Environment.GetEnvironmentVariable("USE_LEGACY_PORTS");
    private static readonly string UseEdgePort = Environment.GetEnvironmentVariable("EDGE_PORT");

    private static void Main(string[] args)
    {
        //Console.WriteLine("Waiting for debugger to attach");
        //while (!Debugger.IsAttached)
        //{
        //    Thread.Sleep(100);
        //}
        //Console.WriteLine("Debugger attached");

        string localStackHost = !string.IsNullOrEmpty(LocalStackHost) ? LocalStackHost : Constants.LocalStackHost;
        bool useSsl = !string.IsNullOrEmpty(UseSsl) && UseSsl is "1" or "true";
        bool useLegacyPorts = !string.IsNullOrEmpty(UseLegacyPorts) || UseLegacyPorts is "1" or "true";
        int edgePort = int.TryParse(UseEdgePort, out int port) ? port : Constants.EdgePort;

        var configOptions = new ConfigOptions(localStackHost, useSsl, useLegacyPorts, edgePort);

        var processRunner = new ProcessRunner();
        var config = new Config(configOptions);
        var fileSystem = new FileSystem();

        var commandDispatcher = new CommandDispatcher(processRunner, config, fileSystem, args);

        commandDispatcher.Run();
    }
}