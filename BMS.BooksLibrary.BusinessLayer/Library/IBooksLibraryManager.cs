using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BMS.BooksLibrary.BusinessLayer.Models;

namespace BMS.BooksLibrary.BusinessLayer
{
    public interface IBooksLibraryManager
    {
        Task<List<BooksCategoryModel>> GetAllBooksCategories();
        Task<bool> SaveBooksCategory(List<BooksCategoryModel> booksCategories);
        Task<bool> SaveBooks(List<BookModel> books);
        Task<bool> SaveImageFile(Stream contentStream, string folder, string fileName);

        Task<List<BookModel>> SearchForBooks(SearchForBookModel searchForBookModel);
    }


}