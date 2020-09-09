using System.Collections.Generic;

namespace BMS.Library.BusinessLayer.Models
{
    public class CategoryBooks
    {
        public BooksCategory BooksCategory { get; set; }
        public string Location { get; set; }
        public IEnumerable<BooksModel> Books { get; set; }
        public string Comments { get; set; }
    }
}