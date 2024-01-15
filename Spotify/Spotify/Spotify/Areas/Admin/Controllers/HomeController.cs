using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotify.Models;
using Spotify.Models.context;
using Spotify.ViewModels;

namespace Spotify.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UploadSong()
        {
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> UploadSong(UploadSong uploadSong)
        {
            if (!ModelState.IsValid)
                return View(uploadSong);

            var artist = await _context.Artists
                .FirstOrDefaultAsync(a => a.Name == uploadSong.ArtistName);
            if (artist == null)  
                return View(uploadSong);
            var song = new Song
            {
                Title = uploadSong.Title,
                Artist = artist,
                ArtistId = artist.Id,
                CoverURL = uploadSong.CoverURL,
                AudioURL = uploadSong.AudioURL,
                Duration = uploadSong.Duration,
            };
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home", new { area = "Admin"  });
        }
    }
}
