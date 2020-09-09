using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BMS.AWS;
using static Newtonsoft.Json.JsonConvert;

namespace BMS.BooksLibrary.BusinessLayer
{
    public class BooksLibraryManager : IBooksLibraryManager
    {
        private const string LibraryCategoryFolder = @"library";
        private const string CategoryFileName = "BooksCategory.json";
        private readonly IS3Uploader _s3Bucket;

        public BooksLibraryManager(IS3Uploader s3Bucket)
        {
            _s3Bucket = s3Bucket;
        }

        public async Task<List<BooksCategoryModel>> GetAllBooksCategories()
        {
            var bookCategoryJson = await _s3Bucket.GetFileFromS3($"{LibraryCategoryFolder}/{CategoryFileName}");

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
                return await _s3Bucket.SaveFileAsync($"{LibraryCategoryFolder}/{CategoryFileName}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}