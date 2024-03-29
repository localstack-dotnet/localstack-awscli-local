﻿/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park, Dmytro Dziuma
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/IFile.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.Contracts;

/// <summary>
/// Represents a file.
/// </summary>
public interface IFile : IFileSystemInfo
{
    /// <summary>
    /// Gets the path to the file.
    /// </summary>
    /// <value>The path.</value>
    new FilePath Path { get; }

    /// <summary>
    /// Gets the length of the file.
    /// </summary>
    /// <value>The length of the file.</value>
    long Length { get; }

    /// <summary>
    /// Gets or sets the file attributes.
    /// </summary>
    /// <value>The file attributes.</value>
    FileAttributes Attributes { get; set; }

    /// <summary>
    /// Copies the file to the specified destination path.
    /// </summary>
    /// <param name="destination">The destination path.</param>
    /// <param name="overwrite">Will overwrite existing destination file if set to <c>true</c>.</param>
    void Copy(FilePath destination, bool overwrite);

    /// <summary>
    /// Moves the file to the specified destination path.
    /// </summary>
    /// <param name="destination">The destination path.</param>
    void Move(FilePath destination);

    /// <summary>
    /// Deletes the file.
    /// </summary>
    void Delete();

    /// <summary>
    /// Opens the file using the specified options.
    /// </summary>
    /// <param name="fileMode">The file mode.</param>
    /// <param name="fileAccess">The file access.</param>
    /// <param name="fileShare">The file share.</param>
    /// <returns>A <see cref="Stream"/> to the file.</returns>
    Stream Open(FileMode fileMode, FileAccess fileAccess, FileShare fileShare);
}