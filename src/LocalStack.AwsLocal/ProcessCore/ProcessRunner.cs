using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using LocalStack.AwsLocal.Contracts;
using LocalStack.AwsLocal.Extensions;
using LocalStack.AwsLocal.ProcessCore.IO;

namespace LocalStack.AwsLocal.ProcessCore
{
/// <summary>
    /// Responsible for starting processes.
    /// </summary>
    public sealed class ProcessRunner : IProcessRunner
    {
        /// <summary>
        /// Starts a process using the specified information.
        /// </summary>
        /// <param name="filePath">The file name such as an application or document with which to start the process.</param>
        /// <param name="settings">The information about the process to start.</param>
        /// <returns>A process handle.</returns>
        public IProcess? Start(FilePath filePath, ProcessSettings settings)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            ProcessStartInfo info = GetProcessStartInfo(filePath, settings, out Func<string, string> filterUnsafe);

            // Start and return the process.
            var process = Process.Start(info);
            if (process == null)
            {
                return null;
            }

            var processWrapper = new ProcessWrapper(process, filterUnsafe, settings.RedirectedStandardOutputHandler,
                filterUnsafe, settings.RedirectedStandardErrorHandler);

            if (settings.RedirectStandardOutput)
            {
                SubscribeStandardOutput(process, processWrapper);
            }
            if (settings.RedirectStandardError)
            {
                SubscribeStandardError(process, processWrapper);
            }

            return processWrapper;
        }

        internal ProcessStartInfo GetProcessStartInfo(FilePath filePath, ProcessSettings settings, out Func<string, string> filterUnsafe)
        {
            // Get the fileName
            var fileName = !RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? filePath.FullPath : filePath.FullPath.Quote();

            // Get the arguments.
            var arguments = settings.Arguments ?? new ProcessArgumentBuilder();
            filterUnsafe = arguments.FilterUnsafe;

            if (!settings.Silent)
            {
                // Log the filename and arguments.
                var message = string.Concat(fileName, " ", arguments.RenderSafe().TrimEnd());
                Console.WriteLine("Executing: {0}", message);
            }

            // Create the process start info.
            var info = new ProcessStartInfo(fileName)
            {
                Arguments = arguments.Render(),
                UseShellExecute = false,
                RedirectStandardError = settings.RedirectStandardError,
                RedirectStandardOutput = settings.RedirectStandardOutput
            };

            // Allow working directory?
            if (!settings.NoWorkingDirectory)
            {
                DirectoryPath? workingDirectory = settings.WorkingDirectory;
                info.WorkingDirectory = workingDirectory?.MakeAbsolute(workingDirectory).FullPath;
            }

            if (settings.EnvironmentVariables == null)
            {
                return info;
            }

            foreach ((string key, string value) in settings.EnvironmentVariables)
            {
                ProcessHelper.SetEnvironmentVariable(info, key, value);
            }

            return info;
        }

        private static void SubscribeStandardError(Process process, ProcessWrapper processWrapper)
        {
            process.ErrorDataReceived += (s, e) =>
            {
                processWrapper.StandardErrorReceived(e.Data);
            };
            process.BeginErrorReadLine();
        }

        private static void SubscribeStandardOutput(Process process, ProcessWrapper processWrapper)
        {
            process.OutputDataReceived += (s, e) =>
            {
                processWrapper.StandardOutputReceived(e.Data);
            };
            process.BeginOutputReadLine();
        }
    }
}
