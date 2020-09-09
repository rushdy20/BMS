using System.Collections.Generic;
using BMS.BooksLibrary.BusinessLayer;

namespace BMS_dotnet_WebApplication.Models.LibraryVM
{
    public class AdminBooksCategoryVM
    {
        public BooksCategoryVM BooksCategory { get; set; }
        public List<BooksCategoryModel> ExistingCategories { get; set; }
    }
}