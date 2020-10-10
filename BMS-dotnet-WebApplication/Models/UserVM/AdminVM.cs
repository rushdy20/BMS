using System.Collections.Generic;
using BMS.BusinessLayer.Library.Models;
using BMS.BusinessLayer.Users.Models;

namespace BMS_dotnet_WebApplication.Models.UserVM
{
    public class AdminVM
    {
        public List<LendingRequestModel> BookLendingRequests { get; set; }
        public List<RegistrationModel> RegistrationWaitingToBeApproved { get; set; }
    }
}