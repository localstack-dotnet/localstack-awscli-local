namespace LocalStack.AwsLocal.Extensions;

/// <summary>
/// Contains extensions for <see cref="IFileSystem"/>.
/// </summary>
public static class FileSystemExtensions
{
    /// <summary>
    /// Determines if a specified <see cref="FilePath"/> exist.
    /// </summary>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="path">The file system.</param>
    /// <returns>Whether or not the specified file exist.</returns>
    public static bool Exist(this IFileSystem fileSystem, FilePath path)
    {
        if (fileSystem == null)
        {
            throw new ArgumentNullException(nameof(fileSystem));
        }
        IFile file = fileSystem.GetFile(path);
        
        return file is { Exists: true };
    }

    /// <summary>
    /// Determines if a specified <see cref="DirectoryPath"/> exist.
    /// </summary>
    /// <param name="fileSystem">The file system.</param>
    /// <param name="path">The path.</param>
    /// <returns>Whether or not the specified directory exist.</returns>
    public static bool Exist(this IFileSystem fileSystem, DirectoryPath path)
    {
        if (fileSystem == null)
        {
            throw new ArgumentNullException(nameof(fileSystem));
        }
        IDirectory directory = fileSystem.GetDirectory(path);
        
        return directory is { Exists: true };
    }
}