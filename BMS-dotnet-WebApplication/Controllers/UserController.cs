using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BMS.BusinessLayer.Constant;
using BMS.BusinessLayer.Users;
using BMS.BusinessLayer.Users.Models;
using BMS_dotnet_WebApplication.Models.UserVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BMS_dotnet_WebApplication.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public UserController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {

            if (string.IsNullOrEmpty(IsLoggedIn()))
                return RedirectToAction("Login", "User");

            var model = await BuildUserProfile(IsLoggedIn());
            return View(model);
        }

        public IActionResult SignUp()
        {
            var model = new SignUpViewModel
            {
                DateOfBirth = DateTime.Today
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if( await IsEmailExists(model.EmailAddress))
            {
               ModelState.AddModelError("name", "Email has been registered already ");
            }

            if( ! ModelState.IsValid)
                return View(model);

            var userModel = _mapper.Map<RegistrationModel>(model);

            await _userManager.SaveUser(userModel);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Login()
        {
            if (!string.IsNullOrEmpty(IsLoggedIn()))
            {
                return RedirectToAction("Index");
            }
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await IsValidUser(model);
            
            if (user == UserLevel.RegistrationProcess.WrongPassword)
                ModelState.AddModelError("Password","Wrong Password Please try again");

            if (user == UserLevel.RegistrationProcess.NotExits)
                ModelState.AddModelError("name", "Email address not registered Please SignUp ");

            if (!ModelState.IsValid)
                return View(model);

            HttpContext.Session.SetString("Name", model.EmailAddress);


            return RedirectToAction("Index");
        }

        private async Task<string> IsValidUser(LoginViewModel model)
        {
            var user = await _userManager.GetUser(model.EmailAddress);
            if (user == null)
                return UserLevel.RegistrationProcess.NotExits;

            if (!string.Equals(user.Password, model.Password, StringComparison.OrdinalIgnoreCase))
                return UserLevel.RegistrationProcess.WrongPassword;

            if (!user.IsApproved)
                return UserLevel.RegistrationProcess.WaitingToBeApproved;

            return UserLevel.RegistrationProcess.Approved;
        }

        private async Task<bool> IsEmailExists(string email)
        {
            var user = await _userManager.GetUser(email);
            return user != null;

        }

        private async Task<UserProfileVM> BuildUserProfile(string email)
        {
            var user = await _userManager.GetUser(email);
            var userProfile = new UserProfileVM
            {
                Name = $"{user.FirstName} {user.Surname}",
                Email = user.EmailAddress,
                AccessArea = new List<string> {UserLevel.AccessArea.LibraryAdmin}
            };
            return userProfile;
        }
    }
}