using System.Collections.Generic;


namespace BMS.BooksLibrary.BusinessLayer.Models
{
  public  class CategoryBooksModel
    {
        public BooksCategoryModel BooksCategory { get; set; }
        public string Location { get; set; }
        public IEnumerable<BookModel> Books { get; set; }
        public string Comments { get; set; }
    }
}
