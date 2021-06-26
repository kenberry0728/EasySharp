namespace EasySharp.Runtime.Serialization.Json
{
    public class JsonStringConverter<T>
    {
        public virtual T FromString(string resultText)
        {
            return resultText.DeserializeFromJsonString<T>();
        }

        public virtual string ToString(T argument)
        {
            return argument.GetSerializeJsonString();
        }
    }
}