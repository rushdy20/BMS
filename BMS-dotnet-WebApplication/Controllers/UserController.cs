using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BMS.BooksLibrary.BusinessLayer;
using BMS.BusinessLayer;
using BMS.BusinessLayer.Constant;
using BMS.BusinessLayer.Users;
using BMS.BusinessLayer.Users.Models;
using BMS_dotnet_WebApplication.Models.UserVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;


namespace BMS_dotnet_WebApplication.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cacheManager;
        private readonly IBooksLibraryManager _booksLibraryManager;
        private readonly IEmailManager _emailManager;

        public UserController(IUserManager userManager, IMapper mapper, ICacheManager cacheManager, IBooksLibraryManager booksLibraryManager, IEmailManager emailManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _cacheManager = cacheManager;
            _booksLibraryManager = booksLibraryManager;
            _emailManager = emailManager;
        }
        public async Task<IActionResult> Index()
        {

            if (string.IsNullOrEmpty(LoggedInName()))
                return RedirectToAction("Login", "User");
            var model = await BuildUserProfile(LoggedInName());
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
            if (!string.IsNullOrEmpty(LoggedInName()))
            {
                return RedirectToAction("Index");
            }
            var model = new LoginViewModel();
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.SetString("userProfile", "");
            HttpContext.Session.SetString("Name", "");
            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await IsValidUser(model);
            
            switch (user)
            {
                case UserLevel.RegistrationProcess.WrongPassword:
                    ModelState.AddModelError("Password","Wrong Password Please try again");
                    break;
                case UserLevel.RegistrationProcess.NotExits:
                    ModelState.AddModelError("name", "Email address not registered Please SignUp ");
                    break;
            }

            if (!ModelState.IsValid)
                return View(model);

            HttpContext.Session.SetString("Name", model.EmailAddress);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RegistrationWaitingToBeApproved()
        {
            if (string.IsNullOrEmpty(LoggedInName()))
                return RedirectToAction("Login", "User");

            var accessAreas = new List<SelectListItem>
            {
                new SelectListItem(UserLevel.AccessArea.LibraryUser,UserLevel.AccessArea.LibraryUser),
                new SelectListItem(UserLevel.AccessArea.LibraryAdmin,UserLevel.AccessArea.LibraryAdmin),
                new SelectListItem(UserLevel.AccessArea.MagazineAdmin,UserLevel.AccessArea.MagazineAdmin)
            };
            
            ViewBag.AccessAreas = accessAreas;

            if (!GetUserProfile().RegistrationWaitingToBeApproved.Any())
            {
                return RedirectToAction("Index");
            }

            var model  = _mapper.Map<List<SignUpViewModel>>(GetUserProfile().RegistrationWaitingToBeApproved);

            return View(model);

        }

        public async Task<IActionResult> ApproveRegistration(SignUpViewModel model)
        {
            if (string.IsNullOrEmpty(LoggedInName()) || !IsAllowed(UserLevel.AccessArea.LibraryAdmin))
                return RedirectToAction("Login", "User");

            var registrationModel = _mapper.Map<RegistrationModel>(model);
            registrationModel.IsApproved = true;
          var updateUser =  await _userManager.UpdateUser(registrationModel);

          if (!updateUser)
              return RedirectToAction("Index");

          await _emailManager.SendEmail(model.EmailAddress, WelcomeToBicRegistrationApprovedEmail(model.FullName), "BIC Registration");
          var userPf = GetUserProfile();
          userPf.RegistrationWaitingToBeApproved = userPf.RegistrationWaitingToBeApproved.Where(u => u.EmailAddress != model.EmailAddress).ToList();
          var str = SerializeObject(userPf);
          HttpContext.Session.SetString("userProfile", str);

          return RedirectToAction("Index");

        }

        public async Task<string> RecoverPassword (string email)
        {
            if (!await IsEmailExists(email))
            {
                return "emailNotExits";
            }
            return await GetPasswordRecoveryQuestion(email);
        }
        public async Task<string> PasswordHint(string email, string hint)
        {
            if (!await IsEmailExists(email))
            {
                return "emailNotExits";
            }

            return await ValidatePasswordRecovery(email, hint);
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

        private async Task<string> GetPasswordRecoveryQuestion(string email)
        {
            var user = await _userManager.GetUser(email);
            return user.PasswordHintQuestion;
        }

        private async Task<string> ValidatePasswordRecovery(string email, string hint)
        {
            var user = await _userManager.GetUser(email);
            if (user == null)
            {
                return "Invalid-Answer";
            }

            return string.Equals(user.PasswordHintAnswer, hint, StringComparison.CurrentCultureIgnoreCase) ? user.Password : "Invalid-Answer";
        }

        private async Task<UserProfileVM> BuildUserProfile(string email)
        {
            var getFromSession = GetUserProfile();

            if (!string.IsNullOrEmpty(getFromSession.Name))
                return getFromSession;

            var user = await _userManager.GetUser(email);
            var userProfile = new UserProfileVM
            {
                Name = $"{user.FirstName} {user.Surname}",
                Email = user.EmailAddress,
                PhoneNo = user.PhoneNumber,
                IsApproved = user.IsApproved,
                AccessArea = user.AllowedAccessAreas,
                BooksNotReturned = await _booksLibraryManager.BooksOnLoan(email)
            };

            if (userProfile.AccessArea !=null && userProfile.AccessArea.Contains(UserLevel.AccessArea.LibraryAdmin))
            {
                userProfile.BookLendingRequests = await _booksLibraryManager.GetNewLendingRequests();
                userProfile.RegistrationWaitingToBeApproved = await _userManager.RegistrationWaitingToBeApproved();
            }

            var str = JsonConvert.SerializeObject(userProfile);
            HttpContext.Session.SetString("userProfile", str);
            return userProfile;
        }

        private string WelcomeToBicRegistrationApprovedEmail(string name)
        {
            var stringBuilder = new StringBuilder($"Assalamu Alikum {name}");
            stringBuilder.AppendLine("Welcome to BIC you account has been approved Please login to get access to Library");
            return stringBuilder.ToString();
        }
    }
}