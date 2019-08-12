using System.Collections.Generic;
using System.IO;

namespace EasySharpStandard.Wrappers.Core
{
    public interface IDirectoryInfo
    {
        IDirectoryInfo Parent { get; }
        IDirectoryInfo CreateSubdirectory(string path);

        void Create();
        IFileInfo[] GetFiles();
        IFileInfo[] GetFiles(string searchPattern);

        IFileInfo[] GetFiles(string searchPattern, SearchOption searchOption);

        IFileSystemInfo[] GetFileSystemInfos();
        IFileSystemInfo[] GetFileSystemInfos(string searchPattern);

        IFileSystemInfo[] GetFileSystemInfos(string searchPattern, SearchOption searchOption);

        IDirectoryInfo[] GetDirectories();
        IDirectoryInfo[] GetDirectories(string searchPattern);

        IDirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption);

        IEnumerable<IDirectoryInfo> EnumerateDirectories();
        IEnumerable<IDirectoryInfo> EnumerateDirectories(string searchPattern);

        IEnumerable<IDirectoryInfo> EnumerateDirectories(
            string searchPattern,
            SearchOption searchOption);


        IEnumerable<IFileInfo> EnumerateFiles();
        IEnumerable<IFileInfo> EnumerateFiles(string searchPattern);
        IEnumerable<IFileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption);

        IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos();
        IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(string searchPattern);

        IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(
            string searchPattern,
            SearchOption searchOption);

        IDirectoryInfo Root { get; }
        void MoveTo(string destDirName);
        void Delete();
        void Delete(bool recursive);
    }
}