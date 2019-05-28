using System.Collections.Generic;

namespace LocalStack.AwsLocal.Contracts
{
    public interface IProcessHelper
    {
        int CmdExecute(string command, string workingDirectoryPath, bool output = true, bool waitForExit = true, IDictionary<string, string> environmentVariables = null);
    }
}