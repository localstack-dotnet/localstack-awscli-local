/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park, Pascal Berger, James Nail, Nathan Clarke
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/IProcess.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.Contracts;

/// <summary>
/// Represents a process.
/// </summary>
public interface IProcess : IDisposable
{
    /// <summary>
    /// Waits for the process to exit.
    /// </summary>
    void WaitForExit();

    /// <summary>
    /// Waits for the process to exit with possible timeout for command.
    /// </summary>
    /// <param name="milliseconds">The amount of time, in milliseconds, to wait for the associated process to exit. The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system.</param>
    /// <returns>true if the associated process has exited; otherwise, false.</returns>
    bool WaitForExit(int milliseconds);

    /// <summary>
    /// Gets the exit code of the process.
    /// </summary>
    /// <returns>The exit code of the process.</returns>
    int GetExitCode();

    /// <summary>
    /// Get the standard error of process.
    /// </summary>
    /// <returns>Returns process error output if <see cref="ProcessSettings.RedirectStandardError">RedirectStandardError</see> is true</returns>
    IEnumerable<string> GetStandardError();

    /// <summary>
    /// Get the standard output of process
    /// </summary>
    /// <returns>Returns process output if <see cref="ProcessSettings.RedirectStandardOutput">RedirectStandardOutput</see> is true</returns>
    IEnumerable<string> GetStandardOutput();

    /// <summary>
    /// Immediately stops the associated process.
    /// </summary>
    void Kill();
}