using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spotify.Models;
using Spotify.Models.context;
using Spotify.Services;
using Spotify.ViewModels;

namespace Spotify.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUpload _upload;
        private readonly UserManager<User> _userManager;

        public AdminHomeController(AppDbContext context, IUpload upload,
            UserManager<User> userManager)
        {
            _context = context;
            _upload = upload;
            _userManager = userManager;
        }
        public async Task<IActionResult> AdminPanel()
        {
            var user = await _userManager.GetUserAsync(User);
            var songs = _context.Songs.Where(s => s.AdminID == user.Id).ToList();
            foreach (var song in songs)
            {
               song.Artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == song.ArtistId);
            }
            return View(songs);
        }
        [HttpGet]
        public IActionResult UploadSong()
        {
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> UploadSong(UploadSong uploadSong, IFormFile musicFile)
        {
            if (!ModelState.IsValid)
                return View(uploadSong);
            if (musicFile != null && musicFile.Length > 0)
            {
                // Call the UploadObjectFromFile method of the MusicStorageService
                var songUrl = _upload.UploadObjectFromFile(musicFile, uploadSong.Title);

                // Check if the upload was successful
                if (songUrl != null)
                {
                    // Store the song URL in the uploadSong object or use it as needed
                    uploadSong.AudioURL = songUrl;
                }
            }
                var artist = await _context.Artists
                .FirstOrDefaultAsync(a => a.Name == uploadSong.ArtistName);
            if (artist == null)  
                return View(uploadSong);
            var user = await _userManager.GetUserAsync(User);
            var song = new Song
            {
                Title = uploadSong.Title,
                Artist = artist,
                ArtistId = artist.Id,
                CoverURL = uploadSong.CoverURL,
                AudioURL = uploadSong.AudioURL,
                Duration = uploadSong.Duration,
                AdminID = user.Id,
            };
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminPanel", "AdminHome");
        }
    }
}
