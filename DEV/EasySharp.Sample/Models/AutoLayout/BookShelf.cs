using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Models.Rails.Core;
using System.ComponentModel;

namespace EasySharp.Sample.Models.AutoLayout
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Need for Save/Load?")]
    public class BookShelf : RailsModel
    {
        [DisplayName("棚番号")]
        [RailsDataMemberBind]
        public int ShelfNumber { get; set; }

        [DisplayName("本")]
        [RailsDataMemberListBind(typeof(Book))]
        public RailsCollection<Book> Books { get; set; } = new RailsCollection<Book>();
    }
}
