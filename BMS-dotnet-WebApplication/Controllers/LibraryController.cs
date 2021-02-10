using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BMS.BooksLibrary.BusinessLayer;
using BMS.BooksLibrary.BusinessLayer.Models;
using BMS.BusinessLayer.Constant;
using BMS.BusinessLayer.Library.Models;
using BMS_dotnet_WebApplication.Models.LibraryVM;
using BMS_dotnet_WebApplication.Models.UserVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;

namespace BMS_dotnet_WebApplication.Controllers
{
    public class LibraryController : BaseController
    {
        private const string BooksImagesFolder = "booksimg";
        private readonly Random _random;
        private List<BooksCategoryModel> _booksCategories;
        private readonly IBooksLibraryManager _booksLibraryManager;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        private readonly Random _rnd;

        public LibraryController(IBooksLibraryManager booksLibraryManager, ICacheManager cacheManager, IMapper mapper)
        {
            _random = new Random();
            _booksLibraryManager = booksLibraryManager;
            _cacheManager = cacheManager;
            _mapper = mapper;
            _rnd = new Random();
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(LoggedInName()))
                return RedirectToAction("Login", "User");

            return View();
        }

        public async Task<IActionResult> BooksCategory()
        {
            if (string.IsNullOrEmpty(LoggedInName())|| !IsAllowed(UserLevel.AccessArea.LibraryAdmin))
                return RedirectToAction("Login", "User");

            _booksCategories = await GetBooksCategories();

            var model = new AdminBooksCategoryVM
            {
                BooksCategory = new BooksCategoryVM(),
                ExistingCategories = _booksCategories
            };
            return View(model);
        }

        public async Task<IActionResult> RegisterBook()
        {
            if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.LibraryAdmin))
                return RedirectToAction("Login", "User");

            _booksCategories = await GetBooksCategories();
            var model = new RegisterBookVM
            {
                Categories = _booksCategories.Select(g => new SelectListItem {Text = g.Name, Value = g.CategoryId}).ToList(),
                NewsBooks = await GetBooks()
            };

            return View(model);
        }

        public async Task<ActionResult> AddCategory(IFormCollection collection)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("BooksCategory");

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

        public async Task<ActionResult> AddBooks(RegisterBookVM model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("BooksCategory");

            var bookModel = _mapper.Map<BookModel>(model);
            bookModel.BookCategory = new BooksCategoryModel
            {
                Name = model.SelectedCategoryText,
                CategoryId = model.SelectedCategory
            };
            bookModel.MainImageFileName = model.MainImageFileName.FileName;

            var cachedBooks = await GetBooks();
            cachedBooks.Add(bookModel);

            await SaveBookImage(model.MainImageFileName);

            _cacheManager.Set("LibraryBooks", cachedBooks);

            return RedirectToAction("RegisterBook");
        }

        public ActionResult Save()
        {
            var cachedBooksCategories = _cacheManager.Get<List<BooksCategoryModel>>("BooksCategories");
            var s3 = UpdateCategoryInS3(cachedBooksCategories).Result;

            return RedirectToAction("Index", "User");
        }

        public ActionResult SaveBook()
        {
            var cachedBooksCategories = _cacheManager.Get<List<BookModel>>("LibraryBooks");
            foreach (var book in cachedBooksCategories) book.IsAvailable = true;

            var s3 = SaveBooksInS3(cachedBooksCategories).Result;
            if (s3)
                _cacheManager.RemoveCache("LibraryBooks");
            return RedirectToAction("Index","User");
        }

        public async Task<IActionResult> SearchBook()
        {
            _booksCategories = await GetBooksCategories();
            var model = new SearchForBookVM
            {
                Categories = _booksCategories.Select(g => new SelectListItem {Text = g.Name, Value = g.CategoryId}).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchBook(SearchForBookVM model)
        {
            _booksCategories = await GetBooksCategories();
            model.SearchedResult = await SearchFromBooks(model);
            model.Categories = _booksCategories.Select(g => new SelectListItem {Text = g.Name, Value = g.CategoryId}).ToList();

            return View(model);
        }

        public async Task<IActionResult> LendingRequest()
        {
            if (string.IsNullOrEmpty(LoggedInName()))
                return RedirectToAction("Login", "User");

            var basket = HttpContext.Session.GetString(LoggedInName());

            if (string.IsNullOrEmpty(basket))
                return RedirectToAction("SearchBook");

            var books = DeserializeObject<List<BookModel>>(basket);

            await _booksLibraryManager.BookLendingRequest(BuildLendingRequest(books));

            var getFromSession = GetUserProfile();
            getFromSession?.BookLendingRequests?.Add(BuildLendingRequest(books));
            var str = JsonConvert.SerializeObject(getFromSession);
            HttpContext.Session.SetString("userProfile", str);

            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> LentOutRequest(bool isRequest)
        {
            if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.LibraryAdmin))
                return RedirectToAction("Login", "User");
            
            var model = new List<LendingRequestModel>(); 

            if (GetUserProfile().BookLendingRequests.Any())
            {
                model = GetUserProfile().BookLendingRequests;
            }
            else
            {
                model = await _booksLibraryManager.GetNewLendingRequests();
            }

            model = isRequest ? model.Where(r => r.LentOn == null).ToList() : model.Where(r => r.LentOn != null).ToList();

            ViewBag.isRequest = isRequest;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LentOut(LendingRequestModel model)
        {
            if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.LibraryAdmin))
                return RedirectToAction("Login", "User");

            var userPf = GetUserProfile();

            var lendingOutRequest = userPf.BookLendingRequests.FirstOrDefault(r => r.LendingRequestId == model.LendingRequestId);
            lendingOutRequest.LentOn = model.LentOn;
            lendingOutRequest.LentBy = userPf.Name;
            lendingOutRequest.Note = model.Note;
            lendingOutRequest.ReturnedOn = model.ReturnedOn;
            lendingOutRequest.IsReadyToCollect = model.IsReadyToCollect;

            if (model.LentOn != null)
            {
                userPf.BookLendingRequests = userPf.BookLendingRequests.Where(r => r.LendingRequestId != model.LendingRequestId).ToList();
            }
            
            if (model.ReturnedOn != null)
            {
                userPf.BookLendingRequests.Add(lendingOutRequest);
            }

            await _booksLibraryManager.BookLendingOut(lendingOutRequest);

            var str = SerializeObject(userPf);
            HttpContext.Session.SetString("userProfile", str);

            return RedirectToAction("Index", "User");
        }

        private LendingRequestModel BuildLendingRequest(List<BookModel> basket)
        {
            var getUserProfile = GetUserProfile();
            return new LendingRequestModel
            {
                LendingRequestId = _rnd.Next(1, 99999),
                RequestedBy = getUserProfile.Name,
                RequestedEmail = getUserProfile.Email,
                PhoneNo = getUserProfile.PhoneNo,
                RequestedDate = DateTime.Today,
                BooksLent = basket
            };
        }

        public int AddBooksToCart(BookModel model)
        {
            if (string.IsNullOrEmpty(LoggedInName()))
                return 0;

            var loggedInName = LoggedInName();

            var basketJson = HttpContext.Session.GetString(loggedInName);
            var basket = new List<BookModel>();
            if (!string.IsNullOrWhiteSpace(basketJson))
            {
                basket = DeserializeObject<List<BookModel>>(basketJson);
            }

            basket.Add(model);

            var str = SerializeObject(basket);
            HttpContext.Session.SetString(loggedInName, str);

            return basket.Count;
        }

        private async Task<List<BookModel>> SearchFromBooks(SearchForBookVM model)
        {
            var searchBookModel = _mapper.Map<SearchForBookModel>(model);

            var searchBooks = await _booksLibraryManager.SearchForBooks(searchBookModel);

            return searchBooks;
        }

        private async Task<List<BooksCategoryModel>> GetBooksCategories()
        {
            try
            {
                var cachedBooksCategories = _cacheManager.Get<List<BooksCategoryModel>>("BooksCategories");

                if (cachedBooksCategories != null && cachedBooksCategories.Any()) return cachedBooksCategories;

                var booksCategories = await _booksLibraryManager.GetAllBooksCategories();

                _cacheManager.Set("BooksCategories", booksCategories);

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

        private async Task<bool> SaveBooksInS3(List<BookModel> model)
        {
            return await _booksLibraryManager.SaveBooks(model);
        }

        private async Task<List<BookModel>> GetBooks()
        {
            try
            {
                var cachedBooks = _cacheManager.Get<List<BookModel>>("LibraryBooks");

                if (cachedBooks != null && cachedBooks.Any()) return cachedBooks;

                cachedBooks = new List<BookModel>();

                _cacheManager.Set("LibraryBooks", cachedBooks);

                return cachedBooks;
            }
            catch (Exception ex)
            {
                //ignore 
            }

            return null;
        }

        private async Task<bool> SaveBookImage(IFormFile imgFormFile)
        {
            if (imgFormFile == null) return false;

            using (var readStream = imgFormFile.OpenReadStream())
            {
                var result = await _booksLibraryManager.SaveImageFile(readStream, BooksImagesFolder, imgFormFile.FileName);

                return result;
            }
        }
    }
}