using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Spotify.Controllers
{
    public class ManegeRoleController : Controller
    {
        private readonly AspNetRoleManager<IdentityRole> _roleManager;
        public ManegeRoleController(AspNetRoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
    }
}
