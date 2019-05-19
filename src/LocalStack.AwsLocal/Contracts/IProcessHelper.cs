using System.Collections.Generic;

namespace LocalStack.AwsLocal.Contracts
{
    internal interface IProcessHelper
    {
        int CmdExecute(string command, string workingDirectoryPath, bool output = true, bool waitForExit = true, IDictionary<string, string> environmentVariables = null);
    }
}