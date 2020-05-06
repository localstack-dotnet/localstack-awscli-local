/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/IProcessRunner.cs
*
***************************************************************************************/


using LocalStack.AwsLocal.ProcessCore;
using LocalStack.AwsLocal.ProcessCore.IO;

namespace LocalStack.AwsLocal.Contracts
{
    /// <summary>
    /// Represents a process runner.
    /// </summary>
    public interface IProcessRunner
    {
        /// <summary>
        /// Starts a process using the specified information.
        /// </summary>
        /// <param name="filePath">The file name such as an application or document with which to start the process.</param>
        /// <param name="settings">The information about the process to start.</param>
        /// <returns>A process handle.</returns>
        IProcess? Start(FilePath filePath, ProcessSettings settings);
    }
}
