﻿using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Spotify.Models;
using Spotify.ViewModels;
using System;
using System.Net;
using System.Web;

namespace Spotify.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly UriBuilder _uriBuilder;
        //private readonly IConfiguration _config;
        private readonly LinkGenerator _linkGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager, LinkGenerator linkGenerator,
            IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            //_config = configuration;
            //_uriBuilder = uriBuilder;
            _linkGenerator = linkGenerator;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> EmailConfirmation(string username, string token)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(username))
                return null;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
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
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user == null)
            {
                // User not found
                return null;
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, lockoutOnFailure: false);
            return result;
        }
        public async Task<IdentityResult> Register(RegisterViewModel registerViewModel)
        {
            var user = new User
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                EmailConfirmed = false
            };
            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                await SendEmail(user);
                if (registerViewModel.Role.Equals("User"))
                    await _userManager.AddToRoleAsync(user, "User");
                else if (registerViewModel.Role.Equals("Admin"))
                    await _userManager.AddToRoleAsync(user, "Admin");
            }
            return result;
        }

        public async Task SendEmail(User user)
        {
            var emailConfirmationToken =
                        await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var urlBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]);
            //var query = HttpUtility.ParseQueryString(_uriBuilder.Query);

            var emailMessage = await GenerateLink(emailConfirmationToken, "Account",
                "ConfirmEmail", user.UserName);
            await _emailSender.SendEmailAsync(user.Email, "Conformation Email", emailMessage);
        }
        public async Task<int> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return 0;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return 0;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var emailMessage = await GenerateLink(token, "Account", "ResetPassword", user.UserName);
            await _emailSender.SendEmailAsync(user.Email, "Reset Password", emailMessage);
            return 1;
        }
        private async Task<string> GenerateLink(string token, string controller, 
            string action, string username)
        {
            var _httpContext = _httpContextAccessor.HttpContext;
            var url = _linkGenerator.GetPathByAction(action: action, controller: controller, 
                new { username = username, token = token });
            var emailMessage = $"{_httpContext.Request.Scheme}://{_httpContext.Request.Host}{url}";
            return emailMessage;
        }
        public async Task<IdentityResult> ResetPassword(string username, string token, 
            string newPassword)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(username))
                return null;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return result;
        }
    }
}
