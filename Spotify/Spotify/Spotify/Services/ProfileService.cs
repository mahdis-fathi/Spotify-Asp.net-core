using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Spotify.Models;

namespace Spotify.Services
{
    public class ProfileService : IProfile
    {
        private readonly UserManager<User> _userManager;
        public ProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<User> GetAsync(string id) 
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }
        public async Task<IdentityResult> EditUser(User user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
            {
                return null;
            }
            if (user.UserName.IsNullOrEmpty()) return null;
            existingUser.UserName = user.UserName;
            var result = await _userManager.UpdateAsync(existingUser);
            return result;
        }
    }
}
