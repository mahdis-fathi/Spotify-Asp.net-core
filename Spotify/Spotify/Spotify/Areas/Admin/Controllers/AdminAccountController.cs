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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                registerViewModel.Role = "Admin";
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
        public IActionResult LoginAdmin()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAdmin(LoginViewModel loginViewModel)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            var result = await _accountService.Login(loginViewModel);
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            if (result.IsLockedOut)
            {
                ViewData["LockedOut"] = "your account is locked out for 5 minutes";
            }
            ModelState.AddModelError(string.Empty, "email or password is wrong");
            return View(loginViewModel);
        }
    }
}
