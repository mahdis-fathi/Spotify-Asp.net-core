using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Classes;

namespace Spotify.Controllers
{
    public class ManegeRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManegeRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roleAdding = new AddRole(_roleManager);
            await roleAdding.AddingRole();
            return View(roleAdding);
        }
    }
}
