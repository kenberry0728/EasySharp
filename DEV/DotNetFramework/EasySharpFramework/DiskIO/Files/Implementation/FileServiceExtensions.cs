using EasySharpStandard.DiskIO.Files.Core;

namespace EasySharpStandard.DiskIO.Files.Implementation
{
    public static class FileServiceExtensions
    {
        public static IFileService Resolve(this IFileService fileService)
        {
            return fileService ?? new FileService();
        }
    }
}