/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Gary Ewan Park
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/IFileSystemInfo.cs
*
***************************************************************************************/

using LocalStack.AwsLocal.ProcessCore.IO;

namespace LocalStack.AwsLocal.Contracts
{
    /// <summary>
    /// Represents an entry in the file system
    /// </summary>
    public interface IFileSystemInfo
    {
        /// <summary>
        /// Gets the path to the entry.
        /// </summary>
        /// <value>The path.</value>
        Path? Path { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IFileSystemInfo"/> exists.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the entry exists; otherwise, <c>false</c>.
        /// </value>
        bool Exists { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IFileSystemInfo"/> is hidden.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the entry is hidden; otherwise, <c>false</c>.
        /// </value>
        bool Hidden { get; }
    }
}