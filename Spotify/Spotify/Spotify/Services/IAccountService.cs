﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;
using Spotify.ViewModels;

namespace Spotify.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(RegisterViewModel registerViewModel);
        Task<Microsoft.AspNetCore.Identity.SignInResult> Login(LoginViewModel loginViewModel);
        Task<User> IsEmailInUse(string email);
        Task<User> IsUsernameInUse(string username);
        Task SendEmail(User user);
    }
}
