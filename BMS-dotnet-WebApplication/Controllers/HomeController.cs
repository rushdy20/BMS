using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BMS.BusinessLayer.Magazine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BMS_dotnet_WebApplication.Models;
using BMS_dotnet_WebApplication.Models.MagazineVM;

namespace BMS_dotnet_WebApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMagazineManager _magazineManager;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, IMagazineManager magazineManager)
        {
            _logger = logger;
            _magazineManager = magazineManager;

           
        }

        public async Task<IActionResult> Index()
        {
            var isItLibrary = IsItLibrary();

            ViewBag.IsItLibrary = isItLibrary;

            if (isItLibrary)
            {
              return  RedirectToAction("Index", "User");
            }

            var model = await BuildIndexVM();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                CurrentEditionImage = $"{currentEdition?.FolderName}/{currentEdition?.Image}"
            };

        }
    }
}
