using EasySharpWpf.Models.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;

namespace EasySharpWpf.Sample.Models.AutoLayout
{
    public class BookShelf : RailsModel
    {
        [RailsBind]
        public RailsList<Book> Books { get; set; } = new RailsList<Book>();
    }
}
