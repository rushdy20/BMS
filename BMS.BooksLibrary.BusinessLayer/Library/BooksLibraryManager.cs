using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BMS.AWS;
using BMS.BooksLibrary.BusinessLayer.Models;
using BMS.BusinessLayer.Library.Models;
using static Newtonsoft.Json.JsonConvert;

namespace BMS.BooksLibrary.BusinessLayer
{
    public class BooksLibraryManager : IBooksLibraryManager
    {
        private const string LibraryCategoryFolder = @"library";
        private const string LibraryBookFolder = @"books";
        private const string CategoryFileName = "BooksCategory.json";
        private const string BooksList = "BooksLisits.json";
        private const string ArchiverLentOutFile = "ArchiverLentOut.json";
        private const string BookLentFile = "BookLent.json";

        private const string BookCategoryCacheKey = "BooksCategories";
        private const string BooksCacheKey = "Books";
        private const string BooksLentCacheKey = "BooksLent";


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
                _cacheManager.Set(BooksCacheKey, allBooks);
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

        public async Task<bool> BookLendingRequest(LendingRequestModel requestModel)
        {
            var lentBooks = await GetAllLentBooks();

            lentBooks.Add(requestModel);

            var jsonString = JsonSerializer.Serialize(lentBooks.OrderBy(o => o.RequestedDate));
            try
            {
                _cacheManager.Set(BooksLentCacheKey, lentBooks.OrderBy(o => o.RequestedDate));
                return await _s3Bucket.SaveFileAsync($"{LibraryCategoryFolder}/{LibraryBookFolder}/{BookLentFile}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> BookLendingOut(LendingRequestModel requestModel)
        {
            

            var getAllNewLendingRequest = await GetNewLendingRequests();

            getAllNewLendingRequest = getAllNewLendingRequest.Where(r => r.LendingRequestId != requestModel.LendingRequestId).ToList();

            if (requestModel.ReturnedOn != null)
            {
                await AddToLendOutArchiver(requestModel);
            }
            else
            {
                getAllNewLendingRequest.Add(requestModel);
            }

            var jsonString = JsonSerializer.Serialize(getAllNewLendingRequest.OrderBy(o => o.RequestedDate));
            try
            {
                _cacheManager.Set(BooksLentCacheKey, getAllNewLendingRequest.OrderBy(o => o.RequestedDate));
                return await _s3Bucket.SaveFileAsync($"{LibraryCategoryFolder}/{LibraryBookFolder}/{BookLentFile}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        

        public async Task<List<LendingRequestModel>> BooksOnLoan(string email)
        {
            var getAllLentBooks = await GetAllLentBooks();
            return getAllLentBooks.Where(u => u.RequestedEmail == email && u.ReturnedOn == null).ToList();
        }

        public async Task<List<LendingRequestModel>> GetNewLendingRequests()
        {
            var allLentBooks = await GetAllLentBooks();
          // return allLentBooks.Where(b => b.LentOn == null).ToList();
          return allLentBooks;
        }

        private async Task AddToLendOutArchiver(LendingRequestModel requestModel)
        {
            var lentBooksFileFromS3Json = await _s3Bucket.GetFileFromS3($"{LibraryCategoryFolder}/{LibraryBookFolder}/{ArchiverLentOutFile}");

            var lentBooksFromS3 = new List<LendingRequestModel>();
            if (!string.IsNullOrEmpty(lentBooksFileFromS3Json))
            {
                lentBooksFromS3 = DeserializeObject<List<LendingRequestModel>>(lentBooksFileFromS3Json);
            }
            lentBooksFromS3.Add(requestModel);

            var jsonString = JsonSerializer.Serialize(lentBooksFromS3.OrderBy(o => o.RequestedDate));
            await _s3Bucket.SaveFileAsync($"{LibraryCategoryFolder}/{LibraryBookFolder}/{ArchiverLentOutFile}", jsonString);

        }

        private async Task<List<BookModel>> GetAllTheBooks()
        {
            var cachedBooks = _cacheManager.Get<List<BookModel>>(BooksCacheKey);

            if (cachedBooks != null && cachedBooks.Any()) return cachedBooks;

            var booksFileFromS3Json =  await _s3Bucket.GetFileFromS3($"{LibraryCategoryFolder}/{LibraryBookFolder}/{BooksList}");

            //"[{\"BookCategory\":{\"CategoryId\":\"3945\",\"Name\":\"Fantasy\",\"Comment\":null},\"Barcode\":\"2525252\",\"Title\":\"Fantasy- world\",\"Publisher\":null,\"MainImageFileName\":\"fl5.png\",\"NumberOfCopies\":10,\"Description\":\"asdfsdaf\",\"Comments\":null},{\"BookCategory\":{\"CategoryId\":\"3945\",\"Name\":\"Fantasy\",\"Comment\":null},\"Barcode\":\"25252365\",\"Title\":\"test book\",\"Publisher\":\"sdgdsfgs\",\"MainImageFileName\":\"image (1).png\",\"NumberOfCopies\":10,\"Description\":\"tes\",\"Comments\":null},{\"BookCategory\":{\"CategoryId\":\"5160\",\"Name\":\"Action and Adventure\",\"Comment\":null},\"Barcode\":\"12300000\",\"Title\":\"Action-1\",\"Publisher\":null,\"MainImageFileName\":\"image (1).png\",\"NumberOfCopies\":10,\"Description\":\"Action-1\",\"Comments\":null},{\"BookCategory\":{\"CategoryId\":\"5160\",\"Name\":\"Action and Adventure\",\"Comment\":null},\"Barcode\":\"002020200\",\"Title\":\"Action-2\",\"Publisher\":\"asdf\",\"MainImageFileName\":\"fl3.jpg\",\"NumberOfCopies\":5,\"Description\":null,\"Comments\":null}]";

            if (string.IsNullOrEmpty(booksFileFromS3Json))
                return new List<BookModel>();

            var categoriesFromS3 = DeserializeObject<List<BookModel>>(booksFileFromS3Json);
            _cacheManager.Set(BooksCacheKey, categoriesFromS3);

            return categoriesFromS3 ?? new List<BookModel>();
        }

        private async Task<List<LendingRequestModel>> GetAllLentBooks()
        {

            //return new List<LendingRequestModel>
            //{
            //    new LendingRequestModel
            //    {
            //        RequestedBy = "Rushdy Najath",
            //        RequestedDate = DateTime.Today.AddDays(-2),
            //        RequestedEmail = "rushdy@yahoo.co.uk",
            //        LentOn = DateTime.Today.AddDays(-5),
            //        Note = "All the books were collected",
            //        BooksLent = new AutoConstructedList<BookModel>
            //        {
            //            new BookModel
            //            {
            //                Barcode = "123456",
            //                Title = "test-123",
            //                MainImageFileName = "thumbnail.jfif"
            //            },
            //            new BookModel
            //            {
            //                Barcode = "120000",
            //                Title = "test-000",
            //                MainImageFileName = "thumbnail.jfif"
            //            }
            //        }
            //    },
            //    new LendingRequestModel
            //    {
            //        RequestedBy = "Rushdy Najath",
            //        RequestedDate = DateTime.Today.AddDays(-5),
            //        RequestedEmail = "rushdy@yahoo.co.uk",
            //        LentOn = DateTime.Today.AddDays(-2),
            //        Note = "Book second-123 was collected from branch-1 and the rest from branch-2 ",
            //        BooksLent = new AutoConstructedList<BookModel>
            //        {
            //            new BookModel
            //            {
            //                Barcode = "8888",
            //                Title = "second-123",
            //                MainImageFileName = "thumbnail.jfif"
            //            },
            //            new BookModel
            //            {
            //                Barcode = "9990000",
            //                Title = "second-000",
            //                MainImageFileName = "thumbnail.jfif"
            //            }
            //        }
            //    },
            //    new LendingRequestModel
            //    {
            //        LendingRequestId = 1258,
            //        RequestedBy = "Rushdy Najath",
            //        RequestedDate = DateTime.Today.AddDays(-5),
            //        RequestedEmail = "rushdy@yahoo.co.uk",
            //        BooksLent = new AutoConstructedList<BookModel>
            //        {
            //            new BookModel
            //            {
            //                Barcode = "8888",
            //                Title = "second-123",
            //                MainImageFileName = "thumbnail.jfif"
            //            },
            //            new BookModel
            //            {
            //                Barcode = "9990000",
            //                Title = "second-000",
            //                MainImageFileName = "thumbnail.jfif"
            //            }
            //        }
            //    },
            //    new LendingRequestModel
            //    {
            //        LendingRequestId = 9528,
            //        RequestedBy = "Yameena Rushdy",
            //        RequestedDate = DateTime.Today.AddDays(-5),
            //        RequestedEmail = "NAJATH@yahoo.co.uk",
            //        BooksLent = new AutoConstructedList<BookModel>
            //        {
            //            new BookModel
            //            {
            //                Barcode = "8888",
            //                Title = "second-123",
            //                MainImageFileName = "thumbnail.jfif"
            //            },
            //            new BookModel
            //            {
            //                Barcode = "9990000",
            //                Title = "second-000",
            //                MainImageFileName = "thumbnail.jfif"
            //            }
            //        }
            //    }
            //};

            var cachedLentBooks = _cacheManager.Get<List<LendingRequestModel>>(BooksLentCacheKey);

            if (cachedLentBooks != null && cachedLentBooks.Any()) return cachedLentBooks;

            var lentBooksFileFromS3Json =  await _s3Bucket.GetFileFromS3($"{LibraryCategoryFolder}/{LibraryBookFolder}/{BookLentFile}");

            if (string.IsNullOrEmpty(lentBooksFileFromS3Json))
                return new List<LendingRequestModel>();

            var lentBooksFromS3 = DeserializeObject<List<LendingRequestModel>>(lentBooksFileFromS3Json);
            _cacheManager.Set(BooksLentCacheKey, lentBooksFromS3);

            return lentBooksFromS3 ?? new List<LendingRequestModel>();
        }
    }
}