using LocalStack.AwsLocal.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LocalStack.AwsLocal
{
    internal class ProcessHelper : IProcessHelper
    {
        public int CmdExecute(string command, string workingDirectoryPath = null, bool output = true, bool waitForExit = true, IDictionary<string, string> environmentVariables = null)
        {
            using (var cmd = new Process())
            {
                bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                cmd.StartInfo.FileName = isWindows ? "cmd.exe" : "bash";

                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.Arguments = isWindows ? $"/c {command}" : "-c \"" + command + "\"";

                if (workingDirectoryPath != null)
                {
                    cmd.StartInfo.WorkingDirectory = workingDirectoryPath;
                }

                if (environmentVariables != null)
                {
                    foreach ((string key, string value) in environmentVariables)
                    {
                        cmd.StartInfo.EnvironmentVariables[key] = value;
                    }
                }

                var returnCode = 0;

                if (output)
                {
                    cmd.OutputDataReceived += (s, e) =>
                    {
                        if (e == null || string.IsNullOrWhiteSpace(e.Data))
                        {
                            return;
                        }

                        if (e.Data.ToLowerInvariant().Contains("error"))
                        {
                            returnCode = 1;
                        }

                        Console.WriteLine(e.Data);
                    };

                    cmd.ErrorDataReceived += (s, e) =>
                    {
                        if (e == null || string.IsNullOrWhiteSpace(e.Data))
                        {
                            return;
                        }

                        if (e.Data.ToLowerInvariant().Contains("error"))
                        {
                            returnCode = 1;
                        }

                        Console.WriteLine(e.Data);
                    };
                }

                cmd.Start();
                cmd.BeginOutputReadLine();
                cmd.BeginErrorReadLine();

                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();

                if (waitForExit)
                {
                    cmd.WaitForExit();
                }

                return returnCode;
            }
        }
    }
}