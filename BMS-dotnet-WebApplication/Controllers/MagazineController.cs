using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BMS.BooksLibrary.BusinessLayer;
using BMS.BusinessLayer;
using BMS.BusinessLayer.Constant;
using BMS.BusinessLayer.Magazine;
using BMS.BusinessLayer.Magazine.Models;
using BMS_dotnet_WebApplication.Models.MagazineVM;
using BMS_dotnet_WebApplication.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMS_dotnet_WebApplication.Controllers
{
    public class MagazineController : BaseController
    {
        private List<MagazineCategory> _magazineCategories;
        private readonly IMagazineManager _magazineManager;
        private readonly ICacheManager _cacheManager;
        private readonly IMapper _mapper;
        private readonly IEmailManager _emailManager;
        private readonly IFeedBack _feedBack;
        private readonly Random _rnd;

        private const string CategoryCacheKey = "MagazineCategories";
        private const string BaseUrl = "http://www.magazine.britanniaislamiccentre.org/";
        private const string CategoryIconFolder = "magazinecategoryicon";

        public MagazineController(IMagazineManager magazineManager, ICacheManager cacheManager, IMapper mapper, IEmailManager emailManager, IFeedBack feedbak)
        {
            _mapper = mapper;
            _magazineManager = magazineManager;
            _cacheManager = cacheManager;
            _emailManager = emailManager;
            _feedBack = feedbak;
            _rnd = new Random();
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.MagazineAdmin))
                return RedirectToAction("Login", "User");

            var model = await BuildIndexVM();

            return View(model);
        }

        public async Task<IActionResult> CreateCategory()
        {
            if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.MagazineAdmin))
                return RedirectToAction("Login", "User");

            var model = new MagazineCategoriesVM
            {
                AddMagazineCategory = new MagazineCategoryVM(),
                ExistingCategories = await GetListOfCategories()
            };

            return View(model);
        }

        public async Task<IActionResult> CurrentEdition(string Id = "CurrentEdition")
        {
            var model = await GetCurrentMagazine(Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(MagazineCategoryVM model)
        {
            if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.MagazineAdmin))
                return RedirectToAction("Login", "User");

            if (!ModelState.IsValid)
                return RedirectToAction("CreateCategory");

            var result = await SaveMagazineImage(model.IconImage, CategoryIconFolder);

            var newCategory = new MagazineCategory
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
            if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.MagazineAdmin))
                return RedirectToAction("Login", "User");

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
                MagazineId = _rnd.Next(1001, 9999).ToString()
            };

            await _magazineManager.CreateMagazine(magazine);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> CreateContent(string Id)
        {
            if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.MagazineAdmin))
                return RedirectToAction("Login", "User");

            var categories = await GetListOfCategories();
            var model = new MagazineContentVM
            {
                NewsCategories = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.CategoryId.ToString() }).ToList(),
                CreatedDate = DateTime.Today.ToString("d"),
                EnteredBy = HttpContext.Session.GetString("Name"),
                Magazine = new MagazineVM { MagazineId = Id }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateContent(MagazineContentVM model)
        {
            if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.MagazineAdmin))
                return RedirectToAction("Login", "User");

            var currentEdition = await _magazineManager.GetMagazine(model.Magazine.MagazineId);

            var magazineContentModel = new MagazineContent
            {
                AuthoredBy = model.Author,
                Body = model.NewsBody,
                Category = new MagazineCategory { CategoryId = model.CategoryId },
                EnteredBy = HttpContext.Session.GetString("Name"),
                EnteredDate = DateTime.Today.ToString(),
                Heading = model.Heading,
                Index = model.Index,
                ContentId = _rnd.Next(0, 99999).ToString(),
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

            if (model.SubImage4 != null)
            {
                magazineContentModel.SubImage4 = model.SubImage4.FileName;
                await SaveMagazineImage(model.SubImage4, currentEdition.FolderName);
            }

            if (model.SubImage5 != null)
            {
                magazineContentModel.SubImage5 = model.SubImage5.FileName;
                await SaveMagazineImage(model.SubImage5, currentEdition.FolderName);
            }

            magazineContentModel.YouTubLink = model.YouTubLink;

            await _magazineManager.AddMagazineContent(magazineContentModel);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id, string mid)
        {
            var model = await GetMagazineContent(id, mid);

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

        public async Task<ActionResult> RemoveContent(string Id)
        {
            var magazine = await _magazineManager.GetMagazine(Id);
            var model = _mapper.Map<RemoveMagazineVM>(magazine);
            model.RemoveContentId = new List<string>();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveContent(string MagazineId, string RemoveContentId)
        {
            var contentIdsToRemove = RemoveContentId.Split(",");
            var magazine = await _magazineManager.RemoveContentsFromCurrentEdition(MagazineId, contentIdsToRemove);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Feedback(FeedbackModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("CurrentEdition");

            var emailStringBuilder = new StringBuilder();
            emailStringBuilder.AppendLine(DateTime.Today.ToShortDateString());
            emailStringBuilder.AppendLine(model.FeedbackOn);
            emailStringBuilder.AppendLine($"From: {model.Name}-{model.EmailAddress}");

            emailStringBuilder.AppendLine(model.FeedBack);

            var feedback = _mapper.Map<BMS.BusinessLayer.Models.FeedbackModel>(model);

            await _feedBack.CreateFeedBack(feedback);

            return RedirectToAction("CurrentEdition");
        }

        public async Task<string> Download(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                try
                {
                    var pathDownload = "c:\\TheSoulJournal";
                    var fileName = key.Split('/').Last();

                    if (!Directory.Exists(pathDownload))
                        Directory.CreateDirectory(pathDownload);

                    var myStringWebResource = $"{CurrentMagazinePath}{key}";
                    using var webClient = new WebClient();
                    webClient.DownloadFile(myStringWebResource, $"{pathDownload}\\{fileName}");
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                return "True"; 
            }

            return "False";
        }

        private async Task<ContentDetailsVM> GetMagazineContent(string contentId, string magazineId)
        {
            var magazine = await _magazineManager.GetMagazine(magazineId);
            var content = magazine.Contents.FirstOrDefault(c => c.ContentId == contentId);
            var contentVM = new ContentDetailsVM
            {
                MagazineCoverImage = magazine.Image,
                Content = content,
                OtherContents = magazine.Contents.Where(c => c.Category.CategoryId == content?.Category.CategoryId && c.ContentId != content?.ContentId).Select(s =>
                        new OtherContent { Heading = s.Heading, Id = s.ContentId, Image = s.MainImage, FolderName = s.FolderName, MagazineId = s.MagazineId })
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

        private async Task<List<MagazineIndexVM>> BuildIndexVM()
        {
            var allMagazines = await _magazineManager.GetAllMagazines();

            var magazineVMs = allMagazines.Select(m => new MagazineIndexVM
            {
                ContentCategories = m.Contents?.Select(c => c.Category).GroupBy(g => g.Name).Select(c => c.FirstOrDefault()).ToList(),
                CurrentEditionName = m.Name,
                CreatedDate = m.DateCreated.ToString("D"),
                MagazineId = m.MagazineId,
                IsLive = m.IsLive,
                CurrentEditionImage = $"{m.FolderName}/{m.Image}"
            }).ToList();

            return magazineVMs;
        }

        private async Task<CurrentEditionVM> GetCurrentMagazine(string Id)
        {
            var magazine = Id == "CurrentEdition" ? await _magazineManager.GetCurrentEdition() : await _magazineManager.GetMagazine(Id);
            magazine.Contents = magazine.Contents != null ? magazine.Contents.OrderBy(d => d.Category.Order).ThenBy(c => c.Index).ToList() : new List<MagazineContent>();
            return new CurrentEditionVM
            {
                Magazine = magazine,
                ContentCategories = magazine?.Contents?.Select(c => c.Category).GroupBy(g => g.Name).Select(c => c.FirstOrDefault()).ToList(),
                IsAdmin = !string.IsNullOrEmpty(LoggedInName()) && IsAllowed(UserLevel.AccessArea.MagazineAdmin)
            };
        }
    }
}