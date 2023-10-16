using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPSAcademy.Data;
using RPSAcademy.Models;
using RPSAcademy.Models.DTOs;
using RPSAcademy.Repository;
using System.Security.Claims;

namespace RPSAcademy.Controllers
{
    public class PlayController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPlayRepository _playRepository;

        public PlayController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IPlayRepository playRepository)
        {
            _context = context;
            _userManager = userManager;
            _playRepository = playRepository;
        }

        public async Task<IActionResult> SelectOpponent()
        {
            // Get the current user's ID
           var user = await _userManager.GetUserAsync(User);
           var userId = user?.Id;

            // Use userId in your query
            var userInfo = _context.UserInfo.FirstOrDefault(u => u.UserId == userId);

            var opponents = await _playRepository.GetOpponents();

            var displayModel = new SelectOpponentDisplayModel
            {
                Opponents = opponents,
                UserInfo = userInfo
            };

            return View(displayModel);
        }
    }
}