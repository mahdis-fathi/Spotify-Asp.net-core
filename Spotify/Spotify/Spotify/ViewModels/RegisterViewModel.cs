using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Spotify.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Remote("IsUsernameInUse", "Account")]
        public string Username { get; set; }
        [EmailAddress]
        [Required]
        [Remote("IsEmailInUse", "Account")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPass { get; set; }
    }
}
