using EasySharpWpf.Models.Rails.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;

namespace EasySharpWpf.Sample.Models.AutoLayout
{
    public class BookShelf : RailsModel
    {
        [RailsBind]
        public RailsList<Book> Books { get; set; } = new RailsList<Book>();
    }
}
