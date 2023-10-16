using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RPSAcademy.Data;
using RPSAcademy.Factories;
using RPSAcademy.Models;
using RPSAcademy.Services;

namespace RPSAcademy.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileService _fileService;
        private readonly IUserInfoFactory _userInfoFactory;


        //change name to userinfocontroller for better understanding.
        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,IFileService fileSerivce, IUserInfoFactory userInfoFactory)
        {
            _userManager = userManager;
            _context = context;
            _fileService = fileSerivce;
            _userInfoFactory = userInfoFactory;
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> CustomizeProfile()
        {
            //Get the User
            var user = await _userManager.GetUserAsync(User);

            //extract the userId
            var userId = user.Id;

            //check if there is a UserInfo linked to the userId, if none will return null
            var UserInfo = _context.UserInfo.FirstOrDefault(u => u.UserId == userId);

            if (UserInfo == null)
            {
                return View("SetProfile");
            }

            return View("CustomizeProfile", UserInfo);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateUserInfo(string prefferedName, IFormFile profileImageFile)
        {
            //Get the User if logged in.
            var user = await _userManager.GetUserAsync(User);

            //extract the userId
            var userId = user.Id;

            //creates a UserInfo object via UserInfoFactory
            var userInfo = _userInfoFactory.CreateUserInfo(userId, prefferedName);

            // Add the UserInfo to the database
            _context.UserInfo.Add(userInfo);
            _context.SaveChanges();

            // if user selected a file to upload
            if (profileImageFile != null)
            {
                //image is saved to www.rootfolder
                var result = _fileService.SaveImage(profileImageFile);
                if (result.Item1 == 1)
                {
                    //updates userinfo
                    userInfo.ProfileImageUrl = result.Item2;
                    //updates userinfo to database
                   _context.SaveChanges();
                }
            }

            return View("CustomizeProfile", userInfo);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateUserInfo(string preferredName, IFormFile imageFile)
        {
            //Get the User if logged in.
            var user = await _userManager.GetUserAsync(User);

            //extract the userId
            var userId = user.Id;

            //fetches UserInfo object linked to the userId
            var userInfo = _context.UserInfo.FirstOrDefault(u => u.UserId == userId);

            //if old name is not same as new name
            if (userInfo.PreferredName != preferredName)
            {
                if(preferredName is not null)
                {
                    userInfo.PreferredName = preferredName;
                    _context.Update(userInfo);
                    _context.SaveChanges();
                }

            }

            if (imageFile != null)
            {
                var result = _fileService.SaveImage(imageFile);
                if (result.Item1 == 1)
                {
                    var oldImage = userInfo.ProfileImageUrl;
                    userInfo.ProfileImageUrl = result.Item2;
                    _context.Update(userInfo);
                    _context.SaveChanges();
                    //deletes the old file in the program if not null
                    if(oldImage is not null)
                    {
                        var deleteResult = _fileService.DeleteImage(oldImage);

                    }
                }
            }

            return View("CustomizeProfile", userInfo);
        }
    }
}