using EasySharpStandard.DiskIO.Directories.Core;

namespace EasySharpStandard.DiskIO.Directories.Implementation
{
    public static class DirectoryServiceExtensions
    {
        public static IDirectoryService Resolve(this IDirectoryService directoryService)
        {
            return directoryService ?? new DirectoryService();
        }
    }
}