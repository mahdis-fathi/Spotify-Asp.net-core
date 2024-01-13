using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Spotify.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Remote("IsUsernameInUse", "Account", HttpMethod="POST", 
            AdditionalFields = "_RequestVerificationToken")]
        public string Username { get; set; }
        public string Role { get; set; }
        [EmailAddress]
        [Required]
        [Remote("IsEmailInUse", "Account", HttpMethod = "POST",
            AdditionalFields = "_RequestVerificationToken")]
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
