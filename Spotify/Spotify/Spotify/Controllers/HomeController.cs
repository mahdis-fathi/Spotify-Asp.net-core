using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Spotify.Models;
using Spotify.Models.context;
using Spotify.Services;
using Spotify.ViewModels;
using System.Diagnostics;

namespace Spotify.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHome _home;
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, IHome home, AppDbContext appDb,
            UserManager<User> userManager)
        {
            _logger = logger;
            _home = home;
            _context = appDb;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var songs = _context.Songs.ToList();
            foreach(var song in songs) 
            {
                song.Artist = await _context.Artists
                    .FirstOrDefaultAsync(a => a.Id == song.ArtistId);
            }
            var user = await _userManager.GetUserAsync(User);
            var favorites = _context.FavoriteSongs
                .Where(fs => fs.UserId == user.Id)
                .Select(fs => fs.SongId)
                .Distinct() // Add distinct to avoid duplicates
                .ToList();

            user.FavoriteSongs.Clear(); // Clear the existing favorite songs collection
            foreach(var favorite in favorites)
            {
                var faveSong = _context.Songs.FirstOrDefault(s => s.Id == favorite);
                if (faveSong != null) user.FavoriteSongs.Add(faveSong);
            }
            var model = new HomeViewModel
            {
                Songs = songs,
                Favorites = user.FavoriteSongs
            };
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}