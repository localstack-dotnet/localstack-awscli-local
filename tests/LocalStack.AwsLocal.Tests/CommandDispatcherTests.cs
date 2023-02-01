namespace LocalStack.AwsLocal.Tests;

public class CommandDispatcherTests
{
    private readonly EnvironmentContextMock _environmentContextMock;
    private readonly ConsoleContextMock _consoleContextMock;

    public CommandDispatcherTests()
    {
        _environmentContextMock = new EnvironmentContextMock();
        _consoleContextMock = new ConsoleContextMock();

        EnvironmentContext.Current = _environmentContextMock;
        ConsoleContext.Current = _consoleContextMock;
    }

    [Fact]
    public void Run_Should_Write_Help_Info_And_Exit_With_Zero_If_Argument_Count_Zero()
    {
        CommandDispatcherMock commandDispatcher = CommandDispatcherMock.Create(Array.Empty<string>());

        commandDispatcher.Run();

        Assert.Equal(0, _environmentContextMock.ExitValue);
        Assert.NotNull(_consoleContextMock.Text);
        Assert.NotEmpty(_consoleContextMock.Text);
    }

    [Fact]
    public void Run_Should_Write_Help_Info_And_Exit_With_Zero_If_The_First_Argument_Is_Help()
    {
        CommandDispatcherMock commandDispatcher = CommandDispatcherMock.Create(new[] {"-h"});

        commandDispatcher.Run();

        Assert.Equal(0, _environmentContextMock.ExitValue);
        Assert.NotNull(_consoleContextMock.Text);
        Assert.NotEmpty(_consoleContextMock.Text);
    }

    [Fact]
    public void Run_Should_Write_Error_And_Exit_With_One_If_The_Arguments_Does_Not_Contains_Valid_ServiceName()
    {
        CommandDispatcherMock commandDispatcher = CommandDispatcherMock.Create(new[] { "-foo -bar" });

        commandDispatcher.Run();

        Assert.Equal(1, _environmentContextMock.ExitValue);
        Assert.NotNull(_consoleContextMock.Text);
        Assert.NotEmpty(_consoleContextMock.Text);
        Assert.StartsWith("ERROR:", _consoleContextMock.Text);
    }

    [Fact]
    public void Run_Should_Write_Error_And_Exit_With_One_If_Given_ServiceName_Has_Not_Valid_LocalStack_Endpoint()
    {
        CommandDispatcherMock commandDispatcher = CommandDispatcherMock.Create(new[] { "hinesis", "list-streams" });

        commandDispatcher.Config
            .Setup(config => config.GetAwsServiceEndpoints())
            .Returns(() => new List<AwsServiceEndpoint>());

        commandDispatcher.Run();

        Assert.Equal(1, _environmentContextMock.ExitValue);
        Assert.NotNull(_consoleContextMock.Text);
        Assert.NotEmpty(_consoleContextMock.Text);
        Assert.StartsWith("ERROR:", _consoleContextMock.Text);

        commandDispatcher.Config
            .Verify(config => config.GetAwsServiceEndpoints(), Times.Once);
    }

    //[Fact]
    //public void Run_Should_Run_Aws_Command_With_Generated_Command_And_Aws_Credentials()
    //{
    //    var args = new[] {"kinesis", "list-streams"};

    //    AwsServiceEndpointMetadata endpointMetadata = AwsServiceEndpointMetadata.Kinesis;
    //    var awsServiceEndpoint = new AwsServiceEndpoint(
    //        endpointMetadata.ServiceId,
    //        endpointMetadata.CliName,
    //        endpointMetadata.Enum,
    //        endpointMetadata.Port,
    //        "localhost",
    //        endpointMetadata.ToString("http", "localhost"));

    //    string cliCommand = args.GetCliCommand(awsServiceEndpoint.ServiceUrl);

    //    CommandDispatcherMock commandDispatcher = CommandDispatcherMock.Create(args);

    //    commandDispatcher.Config
    //        .Setup(config => config.GetAwsServiceEndpoints())
    //        .Returns(() => new[] {awsServiceEndpoint});

    //    commandDispatcher.ProcessRunner.Setup(runner => runner.)

    //    commandDispatcher.ProcessRunner
    //        .Setup(helper => helper.CmdExecute(
    //            It.IsAny<string>(),
    //            It.IsAny<string>(),
    //            It.IsAny<bool>(),
    //            It.IsAny<bool>(),
    //            It.IsAny<Dictionary<string, string>>()))
    //        .Returns(0);

    //    commandDispatcher.Run();

    //    commandDispatcher.Config
    //        .Verify(config => config.GetAwsServiceEndpoints(), Times.Once);

    //    commandDispatcher.ProcessRunner
    //        .Verify(helper => helper.CmdExecute(
    //            It.Is<string>(s => s == cliCommand),
    //            It.Is<string>(s => s == null),
    //            It.Is<bool>(b => b),
    //            It.Is<bool>(b => b),
    //            It.IsAny<Dictionary<string, string>>()), Times.Once);
    //}
}