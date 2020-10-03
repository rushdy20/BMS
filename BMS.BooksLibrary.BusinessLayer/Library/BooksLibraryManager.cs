using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BMS.AWS;
using BMS.BooksLibrary.BusinessLayer.Models;
using static Newtonsoft.Json.JsonConvert;

namespace BMS.BooksLibrary.BusinessLayer
{
    public class BooksLibraryManager : IBooksLibraryManager
    {
        private const string LibraryCategoryFolder = @"library";
        private const string LibraryBookFolder = @"books";
        private const string CategoryFileName = "BooksCategory.json";
        private const string BooksList = "BooksLisits.json";
        private const string BookCategoryCacheKey = "BooksCategories";
        private const string BooksCacheKey = "Books";


        private readonly IS3Uploader _s3Bucket;
        private readonly ICacheManager _cacheManager;
        

        public BooksLibraryManager(IS3Uploader s3Bucket, ICacheManager cacheManager)
        {
            _s3Bucket = s3Bucket;
            _cacheManager = cacheManager;
        }

        public async Task<List<BooksCategoryModel>> GetAllBooksCategories()
        {
            
            var cachedBooksCategories = _cacheManager.Get<List<BooksCategoryModel>>(BookCategoryCacheKey);

            if (cachedBooksCategories != null && cachedBooksCategories.Any()) return cachedBooksCategories;

            var bookCategoryJson = await _s3Bucket.GetFileFromS3($"{LibraryCategoryFolder}/{CategoryFileName}");
            
            _cacheManager.Set(BookCategoryCacheKey, bookCategoryJson);

            if (string.IsNullOrEmpty(bookCategoryJson))
                return new List<BooksCategoryModel>();

            var categoriesFromS3 = DeserializeObject<List<BooksCategoryModel>>(bookCategoryJson);

            return categoriesFromS3 ?? new List<BooksCategoryModel>();
        }

        public async Task<bool> SaveBooksCategory(List<BooksCategoryModel> booksCategories)
        {
            var jsonString = JsonSerializer.Serialize(booksCategories);
            try
            {
                _cacheManager.Set(BookCategoryCacheKey, booksCategories);

                return await _s3Bucket.SaveFileAsync($"{LibraryCategoryFolder}/{CategoryFileName}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> SaveBooks(List<BookModel> books)
        {
            var allBooks = await GetAllTheBooks();
                allBooks.AddRange(books);


            var jsonString = JsonSerializer.Serialize(allBooks.OrderBy(o => o.BookCategory.CategoryId));
            try
            {
                _cacheManager.Set(BooksCacheKey, allBooks.OrderBy(o => o.BookCategory.CategoryId));
                return await _s3Bucket.SaveFileAsync($"{LibraryCategoryFolder}/{LibraryBookFolder}/{BooksList}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> SaveImageFile(Stream contentStream, string folder, string fileName)
        {
            var result = await _s3Bucket.UploadFileAsync($@"{LibraryCategoryFolder}/{folder}/{fileName}", contentStream)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<List<BookModel>> SearchForBooks(SearchForBookModel searchForBookModel)
        {
            var books = await GetAllTheBooks();

            if (!string.IsNullOrEmpty(searchForBookModel.Barcode))
            {
                return books.Where(b => b.Barcode == searchForBookModel.Barcode).ToList();
            }

            if (!string.IsNullOrEmpty(searchForBookModel.Category) && ! string.IsNullOrEmpty(searchForBookModel.Title))
            {
                return books.Where(b => b.BookCategory.Name == searchForBookModel.Category && b.Title.Contains(searchForBookModel.Title)).ToList();
            }

            if (!string.IsNullOrEmpty(searchForBookModel.Category))
            {
                return books.Where(b => b.BookCategory.Name == searchForBookModel.Category).ToList();
            }

            return !string.IsNullOrEmpty(searchForBookModel.Title) ? books.Where(b => b.Title.Contains(searchForBookModel.Title)).ToList() : new List<BookModel>();
        }

        private async Task<List<BookModel>> GetAllTheBooks()
        {
            var cachedBooks = _cacheManager.Get<List<BookModel>>(BooksCacheKey);

            if (cachedBooks != null && cachedBooks.Any()) return cachedBooks;

            var booksFileFromS3Json = await _s3Bucket.GetFileFromS3($"{LibraryCategoryFolder}/{LibraryBookFolder}/{BooksList}");

            if (string.IsNullOrEmpty(booksFileFromS3Json))
                return new List<BookModel>();

            var categoriesFromS3 = DeserializeObject<List<BookModel>>(booksFileFromS3Json);
            _cacheManager.Set(BooksCacheKey, categoriesFromS3);

            return categoriesFromS3 ?? new List<BookModel>();
        }
    }
}