using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BMS_dotnet_WebApplication.Models.MagazineVM
{
    public class MagazineVM
    {
        public string MagazineId { get; set; }
        [Display(Name = "Magazine Name")]
        [Required(ErrorMessage = "Magazine Name is required")]
        public string MagazineName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        [Display(Name = "Main Image")] public IFormFile MainImage { get; set; }

        
    }
}