using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BMS_dotnet_WebApplication.Models.MagazineVM
{
    public class MagazineCategoryVM
    {
        public int CategoryId { get; set; }

        [Required (ErrorMessage = "Category Name is required")]
        public string Name { get; set; }

        public int Order { get; set; }

        [Display(Name = "Icon Image")] public IFormFile IconImage { get; set; }
    }
}
