using EasySharp.IO.Directories.Core;

namespace EasySharp.IO.Directories.Implementation
{
    public static class DirectoryServiceExtensions
    {
        public static IDirectoryService Resolve(this IDirectoryService directoryService)
        {
            return directoryService ?? new DirectoryService();
        }
    }
}