/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park, Dave Glick, Sean Fausett
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/Directory.cs
*
***************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LocalStack.AwsLocal.Contracts;

namespace LocalStack.AwsLocal.ProcessCore.IO
{
    internal sealed class Directory : IDirectory
    {
        private readonly DirectoryInfo _directory;

        public DirectoryPath? Path { get; }

        Path? IFileSystemInfo.Path => Path;

        public bool Exists => _directory.Exists;

        public bool Hidden => (_directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;

        public DateTime LastWriteTime => _directory.LastWriteTime;

        public Directory(DirectoryPath? path)
        {
            Path = path;
            _directory = new DirectoryInfo(Path?.FullPath);
        }

        public void Create()
        {
            _directory.Create();
        }

        public void Move(DirectoryPath destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            _directory.MoveTo(destination.FullPath);
        }

        public void Delete(bool recursive)
        {
            _directory.Delete(recursive);
        }

        public IEnumerable<IDirectory> GetDirectories(string filter, SearchScope scope)
        {
            var option = scope == SearchScope.Current ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories;
            return _directory.GetDirectories(filter, option)
                .Select(directory => new Directory(directory.FullName));
        }

        public IEnumerable<IFile> GetFiles(string filter, SearchScope scope)
        {
            SearchOption option = scope == SearchScope.Current ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories;
            return _directory.GetFiles(filter, option)
                .Select(file => new File(file.FullName));
        }
    }
}