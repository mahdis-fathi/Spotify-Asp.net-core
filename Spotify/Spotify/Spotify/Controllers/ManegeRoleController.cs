using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Classes;
using Spotify.Models;
using Spotify.Models.context;

namespace Spotify.Controllers
{
    public class ManegeRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public ManegeRoleController(RoleManager<IdentityRole> roleManager, AppDbContext appDb)
        {
            _roleManager = roleManager;
            _context = appDb;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var roleAdding = new AddRole(_roleManager);
        //    await roleAdding.AddingRole();
        //    return View(roleAdding);
        //}
        public async Task<IActionResult> AddArtist()
        {
            //var artists = AddArtists.AddArtist();
            //foreach (var artist in artists)
            //{
            //    await _context.Artists.AddAsync(artist);
            //}
            //await _context.SaveChangesAsync();
            return View();
        }
    }
}
