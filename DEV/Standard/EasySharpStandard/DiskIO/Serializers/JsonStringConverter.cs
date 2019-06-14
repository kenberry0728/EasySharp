namespace EasySharpStandard.DiskIO.Serializers
{
    public class JsonStringConverter<T>
    {
        public virtual T ToInstance(string resultText)
        {
            return resultText.DeserializeFromJsonString<T>();
        }

        public virtual string ToString(T argument)
        {
            return argument.GetSerializeJsonString();
        }
    }
}