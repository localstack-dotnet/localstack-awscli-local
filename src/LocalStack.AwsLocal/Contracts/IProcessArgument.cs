/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/IProcessArgument.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.Contracts;

/// <summary>
///  Represents a process argument.
/// </summary>
public interface IProcessArgument
{
    /// <summary>
    /// Render the arguments as a <see cref="string"/>.
    /// Sensitive information will be included.
    /// </summary>
    /// <returns>A string representation of the argument.</returns>
    string Render();

    /// <summary>
    /// Renders the argument as a <see cref="string"/>.
    /// Sensitive information will be redacted.
    /// </summary>
    /// <returns>A safe string representation of the argument.</returns>
    string RenderSafe();
}