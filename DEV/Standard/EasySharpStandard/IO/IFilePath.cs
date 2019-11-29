using System;

namespace EasySharp.IO
{
    public interface IFilePath : IPathObjectBase
    {
        DateTime GetLastWriteTimeUtc();
    }
}