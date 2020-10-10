using System.Collections.Generic;
using BMS.BusinessLayer.Library.Models;

namespace BMS_dotnet_WebApplication.Models.UserVM
{
    public class UserProfileVM : AdminVM
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public List<string> AccessArea { get; set; }
        public List<string> Messages { get; set; }
        public bool IsApproved { get; set; }
        public List<LendingRequestModel> BooksNotReturned { get; set; }
    }
}