using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;
using Spotify.Services;
using Spotify.ViewModels;

namespace Spotify.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<User> _signInManager;
        public AccountController(SignInManager<User> signInManager, IAccountService accountService)
        {
            _signInManager = signInManager;
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Register(registerViewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerViewModel);
        }
        [HttpGet]
        public IActionResult Login() 
        {
            if (_signInManager.IsSignedIn(User)) 
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "Home");
            var result = await _accountService.Login(loginViewModel);
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            if (result.IsLockedOut)
            {
                ViewData["LockedOut"] = "your account is locked out for 5 minutes";
            }
            ModelState.AddModelError( string.Empty, "email or password is wrong");
            return View(loginViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _accountService.IsEmailInUse(email);
            if (user == null)
                return Json(true);
            return Json("email already exists") ;
        }
        public async Task<IActionResult> IsUsernameInUse(string username)
        {
            var user = await _accountService.IsUsernameInUse(username);
            if (user == null)
                return Json(true);
            return Json("username already exists");
        }
    }
}
