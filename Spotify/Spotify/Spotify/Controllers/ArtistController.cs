using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;
using Spotify.Models.context;
using Spotify.Services;

namespace Spotify.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        private readonly IArtist _artist;
        private readonly UserManager<User> _userManager;
        public ArtistController(IArtist artist, UserManager<User> userManager) 
        {
            _artist = artist;
            _userManager = userManager;
        }

        public async Task<IActionResult> FollowingArtist(string artistId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var result = await _artist.FollowArtist(artistId, user);
            if (result == 0) return NotFound();
            return Ok();
        }
    }
}
