using Microsoft.AspNetCore.Identity;

namespace Spotify.Classes
{
    public class AddRole
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddRole(RoleManager<IdentityRole> roleManager)
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
