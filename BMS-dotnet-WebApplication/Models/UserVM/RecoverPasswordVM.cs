using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BMS_dotnet_WebApplication.Models.UserVM
{
    public class RecoverPasswordVM
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress(ErrorMessage = "Please Enter a valid email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Password Hint Answer")]
        [Required(ErrorMessage = "Please Enter  Password Hint Answer")]
        public string Password { get; set; }
    }
}
