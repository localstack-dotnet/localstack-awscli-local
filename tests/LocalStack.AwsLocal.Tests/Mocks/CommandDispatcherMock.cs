using LocalStack.AwsLocal.Contracts;
using LocalStack.Client.Contracts;
using Moq;

namespace LocalStack.AwsLocal.Tests.Mocks
{
    public class CommandDispatcherMock : CommandDispatcher
    {
        public CommandDispatcherMock(Mock<IProcessRunner> processRunner, Mock<IConfig> config, Mock<IFileSystem> fileSystem, string[] args) 
            : base(processRunner.Object, config.Object, fileSystem.Object, args)
        {
            ProcessRunner = processRunner;
            Config = config;
        }

        public Mock<IProcessRunner> ProcessRunner { get; }

        public Mock<IConfig> Config { get; }

        public Mock<IFileSystem> FileSystem { get; }

        public static CommandDispatcherMock Create(string[] args)
        {
            return new CommandDispatcherMock(
                new Mock<IProcessRunner>(MockBehavior.Strict),
                new Mock<IConfig>(MockBehavior.Strict),
                new Mock<IFileSystem>(MockBehavior.Strict), 
                args);
        }
    }
}
