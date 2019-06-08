using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Models.Rails.Core;
using System.ComponentModel;

namespace EasySharp.Sample.Models.AutoLayout
{
    public class BookShelf : RailsModel
    {
        [DisplayName("棚番号")]
        [RailsDataMemberBind]
        public int ShelfNumber { get; set; }

        [DisplayName("本")]
        [RailsDataMemberListBind(typeof(Book))]
        public RailsList<Book> Books { get; set; } = new RailsList<Book>();
    }
}
