using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BMS.BooksLibrary.BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMS_dotnet_WebApplication.Models.LibraryVM
{
    public class RegisterBookVM
    {
        public List<SelectListItem> Categories { get; set; }

        [Display(Name = "Book Category")]
        [Required(ErrorMessage = "Please Select a Category")]
        public string SelectedCategory { get; set; }
        public string SelectedCategoryText { get; set; }

        [Display(Name = "Barcode")]
        [Required(ErrorMessage = "Please Enter Barcode")]
        public string Barcode { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please Enter Title")]
        public string Title { get; set; }

        [Display(Name = "Publisher")] public string Publisher { get; set; }

        [Display(Name = "Cover Image")] public IFormFile MainImageFileName { get; set; }

        [Required(ErrorMessage = "Valid Number")]
        [Display(Name = "Number of Copies")] public int NumberOfCopies { get; set; }

        [Display(Name = "Description")] public string Description { get; set; }

        [Display(Name = "Comment")] public string Comments { get; set; }

        public List<BookModel> NewsBooks { get; set; }
    }
}