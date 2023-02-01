/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/FileSystem.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.ProcessCore.IO;

/// <summary>
/// A physical file system implementation.
/// </summary>
public sealed class FileSystem : IFileSystem
{
    /// <summary>
    /// Gets a <see cref="IFile" /> instance representing the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>A <see cref="IFile" /> instance representing the specified path.</returns>
    public IFile GetFile(FilePath path)
    {
        return new File(path);
    }

    /// <summary>
    /// Gets a <see cref="IDirectory" /> instance representing the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>A <see cref="IDirectory" /> instance representing the specified path.</returns>
    public IDirectory GetDirectory(DirectoryPath path)
    {
        return new Directory(path);
    }
}