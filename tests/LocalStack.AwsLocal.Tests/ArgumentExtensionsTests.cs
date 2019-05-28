using System.Linq;
using LocalStack.AwsLocal.Extensions;
using Xunit;

namespace LocalStack.AwsLocal.Tests
{
    public class ArgumentExtensionsTests
    {
        [Fact]
        public void ExtractServiceName_Should_Return_Empty_String_If_Arguments_Contains_Dash()
        {
            var args = new[] {"-foo", "-bar"};

            string serviceName = args.ExtractServiceName();

            Assert.Equal(string.Empty, serviceName);
        }

        [Fact]
        public void ExtractServiceName_Should_Return_S3_If_One_Of_The_Argument_Is_S3Api()
        {
            var args = new[] { "-foo", "-bar", "s3api" };

            string serviceName = args.ExtractServiceName();

            Assert.Equal("s3", serviceName);
        }

        [Fact]
        public void ExtractServiceName_Should_Extract_First_Valid_Argument_As_ServiceName_From_Arguments()
        {
            var args = new[] { "-foo", "-bar", "kinesis", "list-streams" };

            string serviceName = args.ExtractServiceName();

            Assert.Equal("kinesis", serviceName);
        }

        [Fact]
        public void GetCliCommand_Should_Add_LocalStack_Service_EndPoint_As_Endpoint_Switch_To_Command()
        {
            var args = new[] { "kinesis", "list-streams" };
            const string serviceUrl = "http://localhost:1234";

            string cliCommand = args.GetCliCommand(serviceUrl);

            Assert.Contains($"--endpoint-url={serviceUrl}", cliCommand);
        }

        [Fact]
        public void GetCliCommand_Should_Add_No_Verify_Ssl_Switch_To_Command()
        {
            var args = new[] { "kinesis", "list-streams" };
            const string serviceUrl = "https://localhost:1234";

            string cliCommand = args.GetCliCommand(serviceUrl);

            Assert.Contains("--no-verify-ssl", cliCommand);
        }

        [Fact]
        public void GetCliCommand_Should_Add_Aws_To_Command_As_First_Argument()
        {
            var args = new[] { "kinesis", "list-streams" };
            const string serviceUrl = "http://localhost:1234";

            string cliCommand = args.GetCliCommand(serviceUrl);

            Assert.StartsWith("aws ", cliCommand);
        }

        [Fact]
        public void GetCliCommand_Should_Add_Arguments_To_Command()
        {
            var args = new[] { "-foo", "-bar", "kinesis", "list-streams" };
            const string serviceUrl = "http://localhost:1234";

            string cliCommand = args.GetCliCommand(serviceUrl);

            Assert.Contains(args, s => cliCommand.Split(" ").Contains(s));
        }
    }
}
