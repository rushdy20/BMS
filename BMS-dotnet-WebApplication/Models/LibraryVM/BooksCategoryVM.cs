using System.ComponentModel.DataAnnotations;

namespace BMS_dotnet_WebApplication.Models.LibraryVM
{
    public class BooksCategoryVM
    {
        public string CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Please Enter Category Name")]
        public string Name { get; set; }

        [Display(Name = "Optional Comment")] public string Comment { get; set; }
    }
}