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
    [Authorize]
    public class ArtistController : Controller
    {
        private readonly IArtist _artist;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _Context;
        private readonly IProfile _profile;
        public ArtistController(IArtist artist, UserManager<User> userManager,
            IProfile profile ,AppDbContext appDb) 
        {
            _artist = artist;
            _Context = appDb;
            _userManager = userManager;
            _profile = profile;
        }
        [HttpGet]
        public async Task<IActionResult> ArtistProfile(int id)
        {
            var artist = await _Context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            var user = await _userManager.GetUserAsync(User);
            user.FollowedArtists = _profile.GetArtists(user);
            var model = new ArtistProfie
            {
                ArtistId = id,
                artist = artist,
                user = user
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> FollowingArtist(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var result = await _artist.FollowArtist(id, user);
            if (result == 0) return NotFound();
            var response = new { success = true, message = "Artist followed successfully" };
            return Json(response);
        }
        //[HttpGet]
        //public async Task<IActionResult> UnFollowingArtist(int id)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null) return NotFound();
        //    var result = await _artist.UnfollowArtist(id, user);
        //    if (result == 0) return NotFound();
        //    return RedirectToAction("ArtistProfile", "Artist", new {id = id });
        //}
        [HttpGet]
        public async Task<IActionResult> FollowersList(int id)
        {
            var artist = await _Context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            if (artist == null) return NotFound();
            var result = await _artist.Follwers(artist);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> ArtistsSongs(int id)
        {
            var artist = await _Context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            if (artist == null) return NotFound();
            var result = await _artist.GetSongs(artist);
            return View(result);
        }
    }
}
