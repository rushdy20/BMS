using System.ComponentModel.DataAnnotations;

namespace BMS_dotnet_WebApplication.Models.UserVM
{
    public class LoginViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress(ErrorMessage = "Please Enter a valid email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter a password")]
        public string Password { get; set; }
    }
}