using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMS_dotnet_WebApplication.Models.MagazineVM
{
    public class MagazineContentVM
    {
        public string Id { get; set; }

        public  MagazineVM Magazine { get; set; }

        [Display(Name = "Content Heading")]
        [Required(ErrorMessage = "Heading is required")]
        public string Heading { get; set; }
        public string MainImg { get; set; }

        [Display(Name = "Main Image")] public IFormFile MainImage { get; set; }

        [Display(Name = "Supporting Image-1")] public IFormFile SubImage1 { get; set; }

        [Display(Name = "Supporting Image-2")] public IFormFile SubImage2 { get; set; }

        [Display(Name = "Supporting Image-3")] public IFormFile SubImage3 { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [Display(Name = "Content Body")] public string NewsBody { get; set; }

        [Display(Name = "Created Date")] public string CreatedDate { get; set; }
        public string EnteredBy { get; set; }

        [Required(ErrorMessage = "Heading is Author")]
        [Display(Name = "Author")] public string Author { get; set; }

        [Display(Name = "YouTub Link")] public string YouTubLink { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category is required")]
        public string CategoryId { get; set; }
        public List<SelectListItem> NewsCategories { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string SelectedCategoryId { get; set; }

        [Display(Name = "Index")]
        [Required(ErrorMessage = "Index is required")]
        public int Index { get; set; }
    }
}
