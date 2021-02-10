using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using BMS.BooksLibrary.BusinessLayer;
using BMS.BusinessLayer.Constant;
using BMS.BusinessLayer.Magazine;
using BMS.BusinessLayer.Magazine.Models;
using BMS_dotnet_WebApplication.Models.MagazineVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BMS_dotnet_WebApplication.Controllers
{
    public class MagazineController :  BaseController
    {
        private List<MagazineCategory> _magazineCategories;
        private readonly IMagazineManager _magazineManager;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        private readonly Random _rnd;

        private const string CategoryCacheKey = "MagazineCategories";
        private const string BaseUrl = "www.bic.org";
        private const string CategoryIconFolder = "magazinecategoryicon";

        public MagazineController(IMagazineManager magazineManager, ICacheManager cacheManager, IMapper mapper)
        {
            _mapper = mapper;
            _magazineManager = magazineManager;
            _cacheManager = cacheManager;
            _rnd = new Random();
        }

        public async Task<IActionResult> Index()
        {
            var model = await BuildIndexVM();
           
            return View(model);
        }

        public async Task<IActionResult> CreateCategory()
        {
            //if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.MagazineAdmin))
            //    return RedirectToAction("Login", "User");

            
            var model = new MagazineCategoriesVM
            {
                AddMagazineCategory = new MagazineCategoryVM(),
                ExistingCategories = await GetListOfCategories()
            };

            return View(model); 
        }

        public async Task<IActionResult> CurrentEdition()
        {
            var model = await GetCurrentMagazine();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(MagazineCategoryVM model)
        {
            //if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.MagazineAdmin))
            //    return RedirectToAction("Login", "User");

            if (!ModelState.IsValid)
                return RedirectToAction("CreateCategory");
        
            var result = await SaveMagazineImage(model.IconImage, CategoryIconFolder);

            var newCategory = new MagazineCategory()
           {
                Name = model.Name,
               CategoryId = _rnd.Next(1001, 9999).ToString(),
                Order = model.Order,
                IconImage = model.IconImage.FileName

           };


          if (_magazineCategories == null) _magazineCategories = await GetListOfCategories();
            _magazineCategories.Add(newCategory);
            _cacheManager.Set(CategoryCacheKey, _magazineCategories);

            
            return RedirectToAction("CreateCategory");
        }

        public async Task<IActionResult> SaveCategories()
        {
            var cachedMagazineCategories = _cacheManager.Get<List<MagazineCategory>>(CategoryCacheKey);
            var s3 = await _magazineManager.SaveMagazineCategories(cachedMagazineCategories);

            return RedirectToAction(s3 ? "Index" : "CreateCategory");
        }

        public IActionResult CreateMagazine()
        {
            var model = new MagazineVM
            {
                CreatedBy = LoggedInName(),
                CreatedDate = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMagazine(MagazineVM model)
        {
            //todo add authorisation

            if (!ModelState.IsValid)
                return View(model);

            var folderName = model.MagazineName.Replace(" ", "");
            var result = await SaveMagazineImage(model.MainImage, folderName)
                    .ConfigureAwait(false);
            
            var magazine = new Magazine
            {
                Name = model.MagazineName,
                CreatedBy = HttpContext.Session.GetString("Name"),
                DateCreated = DateTime.Now,
                Image = model.MainImage.FileName,
                FolderName = folderName,
                MagazineId = _rnd.Next(1001, 9999).ToString(),
            };

            await _magazineManager.CreateMagazine(magazine);

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> CreateContent()
        {
            var categories = await GetListOfCategories();
            var model = new MagazineContentVM
            {
                NewsCategories = categories.Select(c => new SelectListItem {Text = c.Name, Value = c.CategoryId.ToString()}).ToList(),
                CreatedDate = DateTime.Today.ToString("d"),
                EnteredBy = HttpContext.Session.GetString("Name"),
                Magazine = new MagazineVM()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateContent(MagazineContentVM model)
        {
            var currentEdition = await _magazineManager.GetCurrentEdition();
            //  var folderPath = CreateFolder(newsId);
            var magazineContentModel = new MagazineContent
            {
                AuthoredBy = model.Author,
                Body = model.NewsBody,
                Category = new MagazineCategory {CategoryId = model.CategoryId},
                EnteredBy = HttpContext.Session.GetString("Name"),
                EnteredDate = DateTime.Today.ToString(),
                Heading = model.Heading,
                Index = model.Index,
                ContentId = _rnd.Next(0,99999).ToString(),
                MagazineId = currentEdition.MagazineId,
                FolderName = currentEdition.FolderName
            };
           
            var stringImages = new List<string>();

            if (model.MainImage != null)
            {
                magazineContentModel.MainImage = model.MainImage.FileName;
                await SaveMagazineImage(model.MainImage, currentEdition.FolderName);
            }
            else
            {
                stringImages.Add("thumbnail.jfif");
            }

            if (model.SubImage1 != null)
            {
                magazineContentModel.SubImage1 = model.SubImage1.FileName;
                await SaveMagazineImage(model.SubImage1, currentEdition.FolderName);
            }

            if (model.SubImage2 != null)
            {
                magazineContentModel.SubImage2 = model.SubImage2.FileName;
                await SaveMagazineImage(model.SubImage2, currentEdition.FolderName);
            }

            if (model.SubImage3 != null)
            {
                magazineContentModel.SubImage3 = model.SubImage3.FileName;
                await SaveMagazineImage(model.SubImage3, currentEdition.FolderName);
            }

            magazineContentModel.YouTubLink = model.YouTubLink;

            await _magazineManager.AddMagazineContent(magazineContentModel);

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Details(string id)
        {
            var model = await GetMagazineContent(id);

            //ViewBag.LoggedIn = false;
            //if (HttpContext.Session.GetString("Name") != null)
            //{
            //    ViewBag.LoggedIn = true;
            //}
            return View(model);
        }


        public async Task<string> GetCurrentEditionImg()
        {
            var magazine = await _magazineManager.GetCurrentEdition();
            return $"{BaseUrl}/{magazine.FolderName}/{magazine.Image}";
        }

        public async Task<ActionResult> StatusUpdate(bool status, string id)
        {
            await _magazineManager.UpdateMagazineStatus(status, id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RemoveContent()
        {
            var magazine = await _magazineManager.GetCurrentEdition();
            var model = _mapper.Map<RemoveMagazineVM>(magazine);
            model.RemoveContentId = new List<string>();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveContent(string RemoveContentId)
        {
            var contentIdsToRemove = RemoveContentId.Split(",");
            var magazine = await _magazineManager.RemoveContentsFromCurrentEdition(contentIdsToRemove);
            

            return  RedirectToAction("Index");
        }
        private async Task<ContentDetailsVM> GetMagazineContent(string contentId)
        {
            var magazine = await _magazineManager.GetCurrentEdition();
            var content = magazine.Contents.FirstOrDefault(c => c.ContentId == contentId);
            var contentVM = new ContentDetailsVM
            {
                Content = content,
                OtherContents = magazine.Contents.Where(c => c.Category.CategoryId == content?.Category.CategoryId && c.ContentId != content?.ContentId).Select(s => new OtherContent {Heading = s.Heading, Id = s.ContentId, Image = s.MainImage, FolderName = s.FolderName})
                    .ToList()
            };

            return contentVM;

        }

        private async Task<List<MagazineCategory>> GetListOfCategories()
        {
            try
            {
                var cachedMagazineCategories = _cacheManager.Get<List<MagazineCategory>>(CategoryCacheKey);

                if (cachedMagazineCategories != null && cachedMagazineCategories.Any()) return cachedMagazineCategories;

                var magazineCategories = await _magazineManager.GetAllMagazineCategories();

                _cacheManager.Set(CategoryCacheKey, magazineCategories);

                return magazineCategories;
            }
            catch (Exception ex)
            {
                //ignore 
            }

            return null;
        }

        private async Task<bool> SaveMagazineImage(IFormFile imgFormFile, string magazineFolder)
        {
            if (imgFormFile == null) return false;

            using (var readStream = imgFormFile.OpenReadStream())
            {
                var result = await _magazineManager.SaveImageFile(readStream, magazineFolder, imgFormFile.FileName);

                return result;
            }
        }

        private async Task<MagazineIndexVM> BuildIndexVM()
        {
            var currentEdition = await _magazineManager.GetCurrentEdition();
            return new MagazineIndexVM
            {
                ContentCategories = currentEdition?.Contents?.Select(c => c.Category).GroupBy(g => g.Name).Select(c => c.FirstOrDefault()).ToList(),
                CurrentEditionName = currentEdition?.Name,
                CreatedDate = currentEdition?.DateCreated.ToString("D"),
                CreatedBy = currentEdition?.CreatedBy,
                MagazineId = currentEdition?.MagazineId,
                CurrentEditionImage = $"{currentEdition?.FolderName}/{currentEdition?.Image}",
                IsLive = currentEdition?.IsLive?? false
            };

        }
        
        
        private async Task<CurrentEditionVM> GetCurrentMagazine()
        {
            var magazine = await _magazineManager.GetCurrentEdition();
            magazine.Contents = magazine.Contents.OrderBy(d => d.Category.Order).ThenBy(c => c.Index).ToList();
            return new CurrentEditionVM
            {
                Magazine = magazine,
                ContentCategories = magazine?.Contents?.Select(c => c.Category).GroupBy(g => g.Name).Select(c => c.FirstOrDefault()).ToList()
            };
        }

    }
}
