using System;
using System.Collections.Generic;

namespace BMS.BusinessLayer.Users.Models
{
    public class RegistrationModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string PostCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsApproved { get; set; }
        public List<string> AllowedAccessAreas { get; set; }
    }
}