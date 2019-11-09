using EasySharp.IO.Files.Core;

namespace EasySharp.IO.Files.Implementation
{
    public static class FileServiceExtensions
    {
        public static IFileService Resolve(this IFileService fileService)
        {
            return fileService ?? new FileService();
        }
    }
}