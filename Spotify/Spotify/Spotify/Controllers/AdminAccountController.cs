using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;
using Spotify.Services;
using Spotify.ViewModels;

namespace Spotify.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<User> _signInManager;
        public AdminAccountController(SignInManager<User> signInManager, IAccountService accountService)
        {
            _signInManager = signInManager;
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            var model = new RegisterViewModel
            {
                Role = "Admin"
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Register(registerViewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerViewModel);
        }
    }
}
