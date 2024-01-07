using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Spotify.Models;
using Spotify.ViewModels;

namespace Spotify.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<User> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }
        public async Task<User> IsUsernameInUse(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user;
        }
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> Login(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email,
                    loginViewModel.Password, loginViewModel.RememberMe, true);
            return result;
        }
        public async Task<IdentityResult> Register(RegisterViewModel registerViewModel)
        {
            var user = new User
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                await SendEmail(user);
            }
            return result;
        }

        public async Task SendEmail(User user)
        {
            var emailConfirmationToken =
                        await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var emailMessage = url.Action("", "Account",
            //    new { username = user.UserName, token = emailConfirmationToken },
            //    request.Scheme);
        }
    }
}
