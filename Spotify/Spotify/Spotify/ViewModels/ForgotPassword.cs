using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Spotify.ViewModels
{
    public class ForgotPassword
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
