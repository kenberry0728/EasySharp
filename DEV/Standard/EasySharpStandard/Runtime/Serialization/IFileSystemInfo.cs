using System.Runtime.Serialization;

namespace EasySharp.Runtime.Serialization
{
    public interface IFileSystemInfo : IO.IFileSystemInfo
    {
        void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}