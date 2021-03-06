﻿using System;
using BMS_dotnet_WebApplication.Models.UserVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Newtonsoft.Json.JsonConvert;

namespace BMS_dotnet_WebApplication.Controllers
{
    public class BaseController : Controller
    {
        private const string CloudFront1 = "http://d1j1ye7fpazrcq.cloudfront.net";
        private const string CloudFront = "http://d2nxbo7tljjo4u.cloudfront.net";
        private const string LibraryFolder = "library";
        private const string magazineFolder = "magazine";

        public BaseController()
        {
          //  ViewBag.IsItLibrary = this.Request.Host.ToString().Contains("bic-library");
        }

        public ActionResult GetImage(string fileName, string section = "default")
        {
            var sectionFolder = LibraryFolder;
            if (section == "magazinecategoryicon")
            {
                sectionFolder = $"{magazineFolder}/{section}";
            }
            else 
            if (section != "default")
            {
                sectionFolder = section;
            }
          

            return Redirect(fileName.EndsWith("thumbnail.jfif") ? $"{CloudFront1}/News/thumbnail.jfif" : $"{CloudFront}/{sectionFolder}/{fileName}");
        }

        public string DownloadUrl(string fileName)
        {
            return $"{CloudFront}/{magazineFolder}/{fileName}";
        }

        internal string LoggedInName()
        {
            return HttpContext.Session.GetString("Name");
        }

        internal UserProfileVM GetUserProfile()
        {
            var userCache = HttpContext.Session.GetString("userProfile");
            if (string.IsNullOrEmpty(userCache))
                return new UserProfileVM();

            var userProfileVm = DeserializeObject<UserProfileVM>(userCache);
            return userProfileVm;
        }

        internal bool IsAllowed(string accessArea)
        {
            var userProfile = GetUserProfile();
            return userProfile.AccessArea.Contains(accessArea);
        }

        protected string CurrentMagazinePath => $"{CloudFront}/{magazineFolder}/";

        internal bool IsItLibrary()
        {
            var isLibrary = this.Request.Host.ToString().Contains("localhost", StringComparison.OrdinalIgnoreCase) || this.Request.Host.ToString().Contains("bic-library", StringComparison.OrdinalIgnoreCase);
           // HttpContext.Session.SetString("bic", isLibrary?"1":"0");
            return isLibrary;
        }
    }
}