using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BMS.BooksLibrary.BusinessLayer
{
   public interface IBooksLibraryManager
   {
       Task<List<BooksCategoryModel>> GetAllBooksCategories();
       Task<bool> SaveBooksCategory(List<BooksCategoryModel> booksCategories);
   }
}
