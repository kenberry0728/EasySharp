namespace EasySharp.IO
{
    public static class DirectoryServiceExtensions
    {
        public static IDirectoryService Resolve(this IDirectoryService directoryService)
        {
            return directoryService ?? new DirectoryService();
        }
    }
}