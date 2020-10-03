using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Validators;

namespace BMS_dotnet_WebApplication.Models.UserVM
{
    public class SignUpViewModel
    {

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress(ErrorMessage = "Please Enter a valid email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter a password")]
        public string Password { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter a password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password are not equal.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please Enter Firstname")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Please Enter Surname")]
        public string Surname { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Please Enter Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please Enter Gender")]
        public string Gender { get; set; }

        [Display(Name = "Address Line1")]
        [Required(ErrorMessage = "Please Enter AddressLine1")]
        public  string AddressLine1 { get; set; }

        [Display(Name = "Address Line2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Address Line3")]
        public string AddressLine3 { get; set; }

        [Display(Name = "Postcode")]
        [Required(ErrorMessage = "Please Enter Postcode")]
        public string PostCode { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
    }
}
