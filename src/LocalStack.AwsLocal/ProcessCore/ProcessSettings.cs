﻿/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park, Pascal Berger, Geoffrey Huntley, Felix, Nathan Clarke
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/ProcessSettings.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.ProcessCore;

/// <summary>
/// Specifies a set of values that are used to start a process.
/// </summary>
public sealed class ProcessSettings
{
    /// <summary>
    /// Gets or sets the set of command-line arguments to use when starting the application.
    /// </summary>
    /// <value>The set of command-line arguments to use when starting the application.</value>
    public ProcessArgumentBuilder Arguments { get; set; }

    /// <summary>
    /// Gets or sets the working directory for the process to be started.
    /// </summary>
    /// <value>The working directory for the process to be started.</value>
    public DirectoryPath WorkingDirectory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not to opt out of using
    /// an explicit working directory for the process.
    /// </summary>
    public bool NoWorkingDirectory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the error output of an application is written to the standard error stream.
    /// </summary>
    /// <value>true if error output should be redirected; false if error output should be written to the standard error stream. The default is false.</value>
    public bool RedirectStandardError { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the output of an application is written to the standard output stream.
    /// </summary>
    /// <value>true if output should be redirected; false if output should be written to the standard output stream. The default is false.</value>
    public bool RedirectStandardOutput { get; set; }

    /// <summary>
    /// Gets or sets a function that intercepts the error output before being redirected. Use in conjunction with <see cref="RedirectStandardError"/>
    /// </summary>
    public Func<string, string> RedirectedStandardErrorHandler { get; set; }

    /// <summary>
    /// Gets or sets a function that intercepts the standard output before being redirected. Use in conjunction with <see cref="RedirectStandardOutput"/>
    /// </summary>
    public Func<string, string> RedirectedStandardOutputHandler { get; set; }

    /// <summary>
    /// Gets or sets optional timeout, in milliseconds, to wait for the associated process to exit. The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system.
    /// </summary>
    public int? Timeout { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether process output will be suppressed.
    /// </summary>
    /// <value>
    ///   <c>true</c> if process output will be suppressed; otherwise, <c>false</c>.
    /// </value>
    public bool Silent { get; set; }

    /// <summary>
    /// Gets or sets search paths for files, directories for temporary files, application-specific options, and other similar information.
    /// </summary>
    /// <example>
    /// <code>
    /// StartProcess("cmd", new ProcessSettings{
    ///         Arguments = "/c set",
    ///         EnvironmentVariables = new Dictionary&lt;string, string&gt;{
    ///             { "CI", "True" },
    ///             { "TEMP", MakeAbsolute(Directory("./Temp")).FullPath }
    ///         }
    ///     });
    /// </code>
    /// </example>
    public IDictionary<string, string> EnvironmentVariables { get; set; }
}