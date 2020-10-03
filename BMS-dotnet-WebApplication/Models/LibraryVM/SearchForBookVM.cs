using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BMS.BooksLibrary.BusinessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMS_dotnet_WebApplication.Models.LibraryVM
{
    public class SearchForBookVM
    {
        public List<SelectListItem> Categories { get; set; }
        public string CategoryName { get; set; }

        [Display(Name = "Barcode")]
        public string Barcode { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        public List<BookModel> SearchedResult { get; set; }
    }
}