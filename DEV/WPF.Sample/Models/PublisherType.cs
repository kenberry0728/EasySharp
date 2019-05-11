using EasySharpStandard.Attributes.Core;

namespace EasySharpWpf.Sample.Models
{
    public enum PublisherType
    {
        [EnumDisplayName("法人")]
        Company,

        [EnumDisplayName("個人")]
        Indivisual
    }
}
