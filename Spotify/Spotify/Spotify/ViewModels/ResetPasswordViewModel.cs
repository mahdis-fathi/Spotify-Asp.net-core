using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spotify.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string RepeatPass { get; set; }
    }
}
