using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spotify.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Username { get; set; }
        public string Token { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
