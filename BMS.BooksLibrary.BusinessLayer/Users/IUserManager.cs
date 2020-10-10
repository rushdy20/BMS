using System.Collections.Generic;
using System.Threading.Tasks;
using BMS.BusinessLayer.Users.Models;

namespace BMS.BusinessLayer.Users
{
    public interface IUserManager
    {
        Task<bool> SaveUser(RegistrationModel userRegistrationModel);
        Task<RegistrationModel> GetUser(string email);
        Task<bool> UpdateUser(RegistrationModel updateRegistrationModel);
        Task<List<RegistrationModel>> RegistrationWaitingToBeApproved();

    }
}