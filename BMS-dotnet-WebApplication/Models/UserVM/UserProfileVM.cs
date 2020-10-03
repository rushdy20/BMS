using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMS_dotnet_WebApplication.Models.UserVM
{
    public class UserProfileVM
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public List<string> AccessArea { get; set; }
        public List<string> Messages { get; set; }
        public bool IsApproved { get; set; }

    }
}
