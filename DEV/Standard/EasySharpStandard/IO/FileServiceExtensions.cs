namespace EasySharp.IO
{
    public static class FileServiceExtensions
    {
        public static IFileService Resolve(this IFileService fileService)
        {
            return fileService ?? new FileService();
        }
    }
}