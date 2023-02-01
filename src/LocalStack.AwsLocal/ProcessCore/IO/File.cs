/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park, Dmytro Dziuma, Sean Fausett
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/File.cs
*
***************************************************************************************/

using System;
using System.IO;
using LocalStack.AwsLocal.Contracts;

namespace LocalStack.AwsLocal.ProcessCore.IO
{
    internal sealed class File : IFile
    {
        private readonly FileInfo _file;

        public FilePath Path { get; }

        Path IFileSystemInfo.Path => Path;

        public bool Exists => _file.Exists;

        public bool Hidden => (_file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;

        public DateTime LastWriteTime => _file.LastWriteTime;

        public long Length => _file.Length;

        public FileAttributes Attributes
        {
            get => _file.Attributes;
            set => _file.Attributes = value;
        }

        public File(FilePath path)
        {
            Path = path;
            _file = new FileInfo(path?.FullPath);
        }

        public void Copy(FilePath destination, bool overwrite)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            _file.CopyTo(destination.FullPath, overwrite);
        }

        public void Move(FilePath destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            _file.MoveTo(destination.FullPath);
        }

        public void Delete()
        {
            _file.Delete();
        }

        public Stream Open(FileMode fileMode, FileAccess fileAccess, FileShare fileShare)
        {
            return _file.Open(fileMode, fileAccess, fileShare);
        }
    }
}