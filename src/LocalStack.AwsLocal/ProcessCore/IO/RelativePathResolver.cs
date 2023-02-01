﻿/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park, Erik Schierboom
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/RelativePathResolver.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.ProcessCore.IO;

internal static class RelativePathResolver
{
    public static DirectoryPath Resolve(DirectoryPath from, DirectoryPath to)
    {
        if (from == null)
        {
            throw new ArgumentNullException(nameof(from));
        }
        if (to == null)
        {
            throw new ArgumentNullException(nameof(to));
        }
        if (to.IsRelative)
        {
            throw new InvalidOperationException("Target path must be an absolute path.");
        }
        if (from.IsRelative)
        {
            throw new InvalidOperationException("Source path must be an absolute path.");
        }
        if (from.Segments.Length == 0 && to.Segments.Length == 0)
        {
            return new DirectoryPath(".");
        }
        if (from.Segments[0] != to.Segments[0])
        {
            throw new InvalidOperationException("Paths must share a common prefix.");
        }

        if (string.CompareOrdinal(from.FullPath, to.FullPath) == 0)
        {
            return new DirectoryPath(".");
        }

        int minimumSegmentsLength = Math.Min(from.Segments.Length, to.Segments.Length);
        var numberOfSharedSegments = 1;

        for (var i = 1; i < minimumSegmentsLength; i++)
        {
            if (string.CompareOrdinal(from.Segments[i], to.Segments[i]) != 0)
            {
                break;
            }

            numberOfSharedSegments++;
        }

        IEnumerable<string> fromSegments = Enumerable.Repeat("..", from.Segments.Length - numberOfSharedSegments);
        IEnumerable<string> toSegments = to.Segments.Skip(numberOfSharedSegments);

        string relativePath = PathHelper.Combine(fromSegments.Concat(toSegments).ToArray());

        if (string.IsNullOrWhiteSpace(relativePath))
        {
            relativePath = ".";
        }

        return new DirectoryPath(relativePath);
    }
}