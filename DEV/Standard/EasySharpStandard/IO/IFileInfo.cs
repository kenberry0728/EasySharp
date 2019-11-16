using System.IO;

namespace EasySharp.IO
{
    public interface IFileInfo : IFileSystemInfo
    {
        long Length { get; }
        string DirectoryName { get; }
        IDirectoryInfo Directory { get; }
        bool IsReadOnly { get; set; }
        StreamReader OpenText();
        StreamWriter CreateText();
        StreamWriter AppendText();
        IFileInfo CopyTo(string destFileName);
        IFileInfo CopyTo(string destFileName, bool overwrite);
        FileStream Create();
        FileStream Open(FileMode mode);
        FileStream Open(FileMode mode, FileAccess access);
        FileStream Open(FileMode mode, FileAccess access, FileShare share);
        FileStream OpenRead();
        FileStream OpenWrite();
        void MoveTo(string destFileName);
        void MoveTo(string destFileName, bool overwrite);
        IFileInfo Replace(string destinationFileName, string destinationBackupFileName);
        IFileInfo Replace(
            string destinationFileName,
            string destinationBackupFileName,
            bool ignoreMetadataErrors);
        void Decrypt();
        void Encrypt();
    }
}