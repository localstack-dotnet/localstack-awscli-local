/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/Directory.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.ProcessCore.IO;

/// <summary>
/// Represents a search scope.
/// </summary>
public enum SearchScope
{
    /// <summary>
    /// The current directory.
    /// </summary>
    Current,

    /// <summary>
    /// The current directory and child directories.
    /// </summary>
    Recursive
}