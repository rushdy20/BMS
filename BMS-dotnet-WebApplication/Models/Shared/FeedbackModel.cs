using System.ComponentModel.DataAnnotations;

namespace BMS_dotnet_WebApplication.Models.Shared
{
    public class FeedbackModel
    {
        public string FeedbackOn { get; set; }

        [Display(Name="Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please Enter a valid email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Feedback")]
        [Required(ErrorMessage = "Feedback cannot be empty")]
        public string FeedBack { get; set; }
    }
}