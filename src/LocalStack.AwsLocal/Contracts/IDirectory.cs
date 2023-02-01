/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park, Dave Glick
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/IDirectory.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.Contracts;

/// Represents a directory.
public interface IDirectory : IFileSystemInfo
{
    /// <summary>
    /// Gets the path to the directory.
    /// </summary>
    /// <value>The path.</value>
    new DirectoryPath Path { get; }

    /// <summary>
    /// Creates the directory.
    /// </summary>
    void Create();

    /// <summary>
    /// Moves the directory to the specified destination path.
    /// </summary>
    /// <param name="destination">The destination path.</param>
    void Move(DirectoryPath destination);

    /// <summary>
    /// Deletes the directory.
    /// </summary>
    /// <param name="recursive">Will perform a recursive delete if set to <c>true</c>.</param>
    void Delete(bool recursive);

    /// <summary>
    /// Gets directories matching the specified filter and scope.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="scope">The search scope.</param>
    /// <returns>Directories matching the filter and scope.</returns>
    IEnumerable<IDirectory> GetDirectories(string filter, SearchScope scope);

    /// <summary>
    /// Gets files matching the specified filter and scope.
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <param name="scope">The search scope.</param>
    /// <returns>Files matching the specified filter and scope.</returns>
    IEnumerable<IFile> GetFiles(string filter, SearchScope scope);
}