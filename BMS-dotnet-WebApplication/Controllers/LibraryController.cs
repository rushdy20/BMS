using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMS.BooksLibrary.BusinessLayer;
using BMS_dotnet_WebApplication.Models.LibraryVM;
using BMS_dotnet_WebApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMS_dotnet_WebApplication.Controllers
{
    public class LibraryController : Controller
    {
        private readonly Random _random;
        private List<BooksCategoryModel> _booksCategories;
        private readonly IBooksLibraryManager _booksLibraryManager;
        private readonly ICacheManager _cacheManager;

        public LibraryController(IBooksLibraryManager booksLibraryManager, ICacheManager cacheManager)
        {
            _random = new Random();
            _booksLibraryManager = booksLibraryManager;
            _cacheManager = cacheManager;
        }

        public IActionResult Index()
        {
            //  if (HttpContext.Session.GetString("Name") == null)      ToDo Add login validation  can go to base controller
            //    return RedirectToAction("Create", "News");

            return View();
        }

        public async Task<IActionResult> BooksCategory()
        {
            //  if (HttpContext.Session.GetString("Name") == null)      ToDo Add login validation can go to base controller
            //    return RedirectToAction("Create", "News");

            _booksCategories = await GetBooksCategories();

            var model = new AdminBooksCategoryVM
            {
                BooksCategory = new BooksCategoryVM(),
                ExistingCategories = _booksCategories
            };
            return View(model);
        }

        public async Task<ActionResult> AddCategory(IFormCollection collection)
        {
            var newCategory = new BooksCategoryModel
            {
                Name = collection["BooksCategory.Name"],
                Comment = collection["BooksCategory.Comment"],
                CategoryId = _random.Next(1001, 9999).ToString()
            };

            if (_booksCategories == null) _booksCategories = await GetBooksCategories();
            _booksCategories.Add(newCategory);
            _cacheManager.Set("BooksCategories", _booksCategories);

            return RedirectToAction("BooksCategory");
        }

        public ActionResult Save()
        {
            var cachedBooksCategories = _cacheManager.Get<List<BooksCategoryModel>>("BooksCategories");
            var s3 = UpdateCategoryInS3(cachedBooksCategories).Result;

            return RedirectToAction("BooksCategory");
        }

        private async Task<List<BooksCategoryModel>> GetBooksCategories()
        {
            try
            {
                var cachedBooksCategories = _cacheManager.Get<List<BooksCategoryModel>>("BooksCategories");

                if (cachedBooksCategories != null && cachedBooksCategories.Any()) return cachedBooksCategories;

                var booksCategories = await _booksLibraryManager.GetAllBooksCategories();

                return booksCategories;
            }
            catch (Exception ex)
            {
                //ignore 
            }

            return null;
        }

        private async Task<bool> UpdateCategoryInS3(List<BooksCategoryModel> model)
        {
            return await _booksLibraryManager.SaveBooksCategory(model);
        }
    }
}