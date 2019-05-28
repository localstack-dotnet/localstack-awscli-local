using System;
using System.Collections.Generic;
using System.Text;
using LocalStack.AwsLocal.Tests.Mocks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace LocalStack.AwsLocal.Tests
{
    public class CommandDispatcherTests
    {
        [Fact]
        public void Run_Should_Write_Help_Info_And_Exit_If_Argument_Count_Zero()
        {
            CommandDispatcherMock commandDispatcherMock = CommandDispatcherMock.Create(new string[0]);


        }
    }
}
