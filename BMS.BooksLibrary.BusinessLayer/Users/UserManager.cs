using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BMS.AWS;
using BMS.BooksLibrary.BusinessLayer;
using BMS.BusinessLayer.Users.Models;
using static Newtonsoft.Json.JsonConvert;

namespace BMS.BusinessLayer.Users
{
  public class UserManager : IUserManager
    {
        private const string UserFolder = @"library";
        private const string UserFileName = "User.json";
        private const string UsersCacheKey = "Users";

        private readonly IS3Uploader _s3Bucket;
        private readonly ICacheManager _cacheManager;
        public UserManager(IS3Uploader s3Bucket, ICacheManager cacheManager)
        {
            _s3Bucket = s3Bucket;
            _cacheManager = cacheManager;
        }

        public async Task<bool> SaveUser(RegistrationModel userRegistrationModel)
        {
            var allUsers = await GetAllUsers();
            allUsers.Add(userRegistrationModel);
            try
            {
                _cacheManager.Set(UsersCacheKey, allUsers);

                var jsonString = JsonSerializer.Serialize(allUsers);

                return await _s3Bucket.SaveFileAsync($"{UserFolder}/{UserFileName}", jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<RegistrationModel> GetUser(string email)
        {
            var allUsers = await GetAllUsers();

            return allUsers.FirstOrDefault(u => u.EmailAddress == email);

        }

        public async Task<bool> UpdateUser(RegistrationModel updateRegistrationModel)
        {
            var allUsers = await GetAllUsers();
            var otherUsers = allUsers.Where(u => u.EmailAddress != updateRegistrationModel.EmailAddress).ToList();
            
            otherUsers.Add(updateRegistrationModel);

            _cacheManager.Set(UsersCacheKey, otherUsers);

            var jsonString = JsonSerializer.Serialize(otherUsers);

            return await _s3Bucket.SaveFileAsync($"{UserFolder}/{UserFileName}", jsonString);

        }

        public async Task<List<RegistrationModel>> RegistrationWaitingToBeApproved()
        {
            var allUsers = await GetAllUsers();
            return allUsers.Where(u => !u.IsApproved).ToList();
        }

        private async Task<List<RegistrationModel>> GetAllUsers()
        {
            var cachedUsers = _cacheManager.Get<List<RegistrationModel>>(UsersCacheKey);

            if (cachedUsers != null && cachedUsers.Any()) return cachedUsers;

            var userFileFromS3Json =
                "[{\"EmailAddress\":\"rushdy@yahoo.co.uk\",\"Password\":\"Yameena20\",\"FirstName\":\"Rushdy\",\"Surname\":\"Najath\",\"DateOfBirth\":\"2020-10-28T00:00:00\",\"Gender\":\"M\",\"AddressLine1\":\"7 Holly Road\",\"AddressLine2\":\"Hansworth\",\"AddressLine3\":\"Birminghamd\",\"PostCode\":\"B\",\"PhoneNumber\":null,\"IsApproved\":false}]";
            //await _s3Bucket.GetFileFromS3($"{UserFolder}/{UserFileName}");

            if (string.IsNullOrEmpty(userFileFromS3Json))
                return new List<RegistrationModel>();

            var usersFromS3 = DeserializeObject<List<RegistrationModel>>(userFileFromS3Json);
            _cacheManager.Set(UsersCacheKey, usersFromS3);

            return usersFromS3 ?? new List<RegistrationModel>();
        }
    }
}
