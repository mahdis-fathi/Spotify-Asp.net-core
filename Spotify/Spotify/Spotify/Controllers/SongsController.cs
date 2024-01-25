using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;
using Spotify.Services;

namespace Spotify.Controllers
{
    [Authorize]
    public class SongsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ISong _song;
        public SongsController(UserManager<User> userManager, ISong song) 
        {
            _userManager = userManager;
            _song = song;
        }
        [HttpPost]
        public async Task<IActionResult> AddFavoriteSongs(int songId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var result = await _song.FavoriteSongs(songId, user);
            if (result == 0)
                return NotFound();
            return Ok();
        }
        //public async Task<IActionResult> DownloadMusic(string songId)
        //{
        //    var content = await _song.DownloadMusic(songId);

        //    // Return the file as a byte array
        //    return File(content.fileBytes, content.contentType, content.fileName);
        //}
    }
}
