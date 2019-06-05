using EasySharp.Sample.Models.AutoLayout;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Models.Rails.Core;
using System.ComponentModel;

namespace EasySharpWpf.Sample.Models.AutoLayout
{
    public class BookShelf : RailsModel
    {
        [DisplayName("棚番号")]
        [RailsBind]
        public int ShelfNumber { get; set; }

        [DisplayName("本")]
        [RailsListBind(typeof(Book))]
        public RailsList<Book> Books { get; set; } = new RailsList<Book>();
    }
}
