/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Sean Fausett, James Nail, Gary Ewan Park, Felix, Nathan Clarke
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/ProcessWrapper.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.ProcessCore;

internal sealed class ProcessWrapper : IProcess
{
    private readonly System.Diagnostics.Process _process;
    private readonly Func<string, string> _filterError;
    private readonly Func<string, string> _filterOutput;
    private readonly ConcurrentQueue<string> _consoleErrorQueue;
    private readonly ConcurrentQueue<string> _consoleOutputQueue;
    private readonly Func<string, string> _standardOutputHandler;
    private readonly Func<string, string> _standardErrorHandler;

    public ProcessWrapper(System.Diagnostics.Process process, Func<string, string> filterOutput,
        Func<string, string> standardOutputHandler,
        Func<string, string> filterError, Func<string, string> standardErrorHandler)
    {
        _process = process;
        _filterOutput = filterOutput ?? (source => "[REDACTED]");
        _consoleOutputQueue = new ConcurrentQueue<string>();
        _standardOutputHandler = standardOutputHandler ?? (output => output);
        _filterError = filterError ?? (source => "[REDACTED]");
        _consoleErrorQueue = new ConcurrentQueue<string>();
        _standardErrorHandler = standardErrorHandler ?? (output => output);
    }

    public void WaitForExit()
    {
        _process.WaitForExit();
    }

    public bool WaitForExit(int milliseconds)
    {
        if (_process.WaitForExit(milliseconds))
        {
            return true;
        }

        _process.Refresh();
        if (!_process.HasExited)
        {
            _process.Kill();
        }

        return false;
    }

    public int GetExitCode()
    {
        return _process.ExitCode;
    }

    internal void StandardErrorReceived(string standardError)
    {
        string redirectedError = _standardErrorHandler(standardError);

        if (redirectedError != null)
        {
            _consoleErrorQueue.Enqueue(redirectedError);
        }
    }

    public IEnumerable<string> GetStandardError()
    {
        while (!_consoleErrorQueue.IsEmpty || !_process.HasExited)
        {
            if (!_consoleErrorQueue.TryDequeue(out string line))
            {
                continue;
            }

            Console.WriteLine("{0}", _filterError(line));
            yield return line;
        }
    }

    internal void StandardOutputReceived(string standardOutput)
    {
        string redirectedOutput = _standardOutputHandler(standardOutput);

        if (redirectedOutput != null)
        {
            _consoleOutputQueue.Enqueue(redirectedOutput);
        }
    }

    public IEnumerable<string> GetStandardOutput()
    {
        while (!_consoleOutputQueue.IsEmpty || !_process.HasExited)
        {
            if (!_consoleOutputQueue.TryDequeue(out string line))
            {
                continue;
            }

            Console.WriteLine("{0}", _filterOutput(line));

            yield return line;
        }
    }

    public void Kill()
    {
        _process.Kill();
        _process.WaitForExit();
    }

    public void Dispose()
    {
        _process.Dispose();
    }
}