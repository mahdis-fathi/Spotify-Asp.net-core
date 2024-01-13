using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Classes;

namespace Spotify.Controllers
{
    public class ManegeRoleController : Controller
    {
        private readonly AspNetRoleManager<IdentityRole> _roleManager;
        public ManegeRoleController(AspNetRoleManager<IdentityRole> roleManager)
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
