using AutoMapper;
using BMS.BusinessLayer.Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMS_dotnet_WebApplication.Controllers
{
    public class BaseController : Controller
    {

        private const string CloudFront1 = "http://d1j1ye7fpazrcq.cloudfront.net"; 
        private const string CloudFront = "http://d2nxbo7tljjo4u.cloudfront.net";
        private const string LibraryFolder = "library";
       

        public ActionResult GetImage(string fileName)
        {
           return Redirect(fileName.EndsWith("thumbnail.jfif") ? $"{CloudFront1}/News/thumbnail.jfif" : $"{CloudFront}/{LibraryFolder}/{fileName}"); 
        }

        internal string IsLoggedIn()
        {
            return HttpContext.Session.GetString("Name");
        }
    }
}