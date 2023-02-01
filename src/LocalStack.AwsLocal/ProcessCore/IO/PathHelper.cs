﻿/***************************************************************************************
*    Title: cake-build/cake
*    Author: Patrik Svensson
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/Path.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.ProcessCore.IO;

internal static class PathHelper
{
    private const char Backslash = '\\';
    private const char Slash = '/';
    private const string UncPrefix = @"\\";

    private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static string Combine(params string[] paths)
    {
        if (paths.Length == 0)
        {
            return string.Empty;
        }

        string current = paths[0];
        
        for (var index = 1; index < paths.Length; index++)
        {
            current = PathHelper.Combine(current, paths[index]);
        }
        return current;
    }

    public static string Combine(string first, string second)
    {
        if (first == null)
        {
            throw new ArgumentNullException(nameof(first));
        }
        if (second == null)
        {
            throw new ArgumentNullException(nameof(second));
        }

        // Both empty?
        if (string.IsNullOrWhiteSpace(first) && string.IsNullOrWhiteSpace(second))
        {
            return string.Empty;
        }
        // First empty?
        if (string.IsNullOrWhiteSpace(first) && !string.IsNullOrWhiteSpace(second))
        {
            return second;
        }
        // Second empty?
        if (!string.IsNullOrWhiteSpace(first) && string.IsNullOrWhiteSpace(second))
        {
            return first;
        }

        bool isUnc = first.StartsWith(UncPrefix);

        // Trim separators.
        first = first.TrimEnd(Backslash, Slash);
        second = second.TrimStart(Backslash, Slash).TrimEnd(Backslash, Slash);

        // UNC root only?
        if (isUnc && string.IsNullOrWhiteSpace(first))
        {
            return string.Concat(UncPrefix, second);
        }

        char separator = isUnc ? Backslash : Slash;
        return string.Concat(first, separator, second);
    }

    public static bool HasExtension(FilePath path)
    {
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        for (int index = path.FullPath.Length - 1; index >= 0; index--)
        {
            if (path.FullPath[index] == '.')
            {
                return true;
            }

            if (path.IsUNC && path.FullPath[index] == '\\')
            {
                break;
            }
            if (path.FullPath[index] == '/')
            {
                break;
            }
        }

        return false;
    }

    public static string GetDirectoryName(FilePath path)
    {
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        switch (path.Segments.Length)
        {
            case 0:
                return string.Empty;
            case 1 when path.IsUNC:
                return @"\\";
            case 1 when path.Segments[0].Length >= 1 && path.Segments[0][0] == '/':
                return "/";
        }

        if (!path.IsUNC)
        {
            return string.Join("/", path.Segments.Take(path.Segments.Length - 1));
        }

        IEnumerable<string> segments = path.Segments.Skip(1).Take(path.Segments.Length - 2);
        return string.Concat(@"\\", string.Join("\\", segments));

    }

    public static string GetFileName(FilePath path)
    {
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (path.Segments.Length == 0)
        {
            return null;
        }

        var filename = path.Segments[path.Segments.Length - 1];
        if (path.Segments.Length == 1 && !path.IsRelative)
        {
            if (path.Segments[0].StartsWith("/"))
            {
                return filename.TrimStart('/');
            }
            if (path.IsUNC)
            {
                return filename.TrimStart('\\');
            }

            return null;
        }

        return filename;
    }

    public static string GetFileNameWithoutExtension(FilePath path)
    {
        var filename = PathHelper.GetFileName(path);
        if (filename == null)
        {
            return null;
        }

        var index = filename.LastIndexOf('.');
        if (index != -1)
        {
            return filename.Substring(0, index);
        }

        return filename;
    }

    public static string ChangeExtension(FilePath path, string extension)
    {
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        if (extension == null)
        {
            return RemoveExtension(path);
        }
        if (extension != null && string.IsNullOrWhiteSpace(extension))
        {
            // Empty extension is an extension consisting of only a period.
            extension = ".";
        }

        // Make sure that the extension has a dot.
        if (extension != null && !extension.StartsWith("."))
        {
            extension = string.Concat(".", extension);
        }

        // Empty path?
        var filename = path.FullPath;
        if (string.IsNullOrWhiteSpace(filename))
        {
            return null;
        }

        for (int index = path.FullPath.Length - 1; index >= 0; index--)
        {
            if (filename[index] == '/')
            {
                // No extension found.
                break;
            }
            if (filename[index] == '.')
            {
                // Replace the extension.
                return string.Concat(filename.Substring(0, index), extension);
            }
        }

        return string.Concat(filename, extension);
    }

    public static string RemoveExtension(FilePath path)
    {
        if (path == null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        for (int index = path.FullPath.Length - 1; index >= 0; index--)
        {
            if (path.FullPath[index] == '.')
            {
                return path.FullPath.Substring(0, index);
            }

            if (path.IsUNC && path.FullPath[index] == '\\')
            {
                break;
            }
            if (path.FullPath[index] == '/')
            {
                break;
            }
        }

        return path.FullPath;
    }

    public static bool IsPathRooted(string path)
    {
        var length = path.Length;
        if (length >= 1)
        {
            // Root?
            if (path[0] == '/')
            {
                return true;
            }

            if (path.Length >= 2)
            {
                // UNC?
                if (path[0] == '\\' && path[1] == '\\')
                {
                    return true;
                }
            }

            if (IsWindows)
            {
                // Windows drive?
                if (length >= 2 && path[1] == ':')
                {
                    return true;
                }
            }
        }

        return false;
    }
}