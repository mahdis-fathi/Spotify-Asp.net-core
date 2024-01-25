using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;
using Spotify.Services;

namespace Spotify.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProfile _profile;
        public ProfileController(IProfile profile, UserManager<User> userManager)
        {
            _userManager = userManager;
            _profile = profile;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var result = await _profile.EditUser(user);
            if (result == null) 
                return View(user);
            if (result.Succeeded)
            {
                ViewData["ErrorMessage"] = "user Successfuly updated!";
                return RedirectToAction("Index", "Profile");
            }
            foreach (var error in result.Errors)
            {
               ModelState.AddModelError("", error.Description);
            }
            return View(user);
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public async Task<IActionResult> FollowedSingers()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var singers = _profile.GetArtists(user);
            return View(singers);
        }
        public async Task<IActionResult> FavoriteSongs()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var songs = _profile.GetSongs(user);
            return View(songs);
        }
    }
}
