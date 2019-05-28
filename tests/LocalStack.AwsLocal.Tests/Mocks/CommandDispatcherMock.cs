using System.IO;
using LocalStack.AwsLocal.Contracts;
using LocalStack.Client.Contracts;
using Moq;

namespace LocalStack.AwsLocal.Tests.Mocks
{
    public class CommandDispatcherMock : CommandDispatcher
    {
        public CommandDispatcherMock(Mock<IProcessHelper> processHelper, Mock<IConfig> config, Mock<TextWriter> textWriter, string[] args) 
            : base(processHelper.Object, config.Object, textWriter.Object, args)
        {
            ProcessHelper = processHelper;
            Config = config;
            TextWriter = textWriter;
        }

        public Mock<IProcessHelper> ProcessHelper { get; }

        public Mock<IConfig> Config { get; }

        public Mock<TextWriter> TextWriter { get; }

        public static CommandDispatcherMock Create(string[] args)
        {
            return new CommandDispatcherMock(
                new Mock<IProcessHelper>(MockBehavior.Strict),
                new Mock<IConfig>(MockBehavior.Strict), 
                new Mock<TextWriter>(MockBehavior.Strict), 
                args);
        }
    }
}
