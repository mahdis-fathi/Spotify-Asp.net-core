using Microsoft.AspNetCore.Identity;

namespace Spotify.Classes
{
    public class AddRole
    {
        private readonly AspNetRoleManager<IdentityRole> _roleManager;
        public AddRole(AspNetRoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task AddingRole()
        {
            var admin = new IdentityRole("Admin");
            var user = new IdentityRole("User");
            await _roleManager.CreateAsync(admin);
            await _roleManager.CreateAsync(user);
        }
    }
}
