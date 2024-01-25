using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Spotify.Models;
using Spotify.Models.context;

namespace Spotify.Services
{
    public class ProfileService : IProfile
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        public ProfileService(UserManager<User> userManager, AppDbContext appDb)
        {
            _userManager = userManager;
            _context = appDb;
        }
        public async Task<User> GetAsync(string id) 
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }
        public async Task<IdentityResult> EditUser(User user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
            {
                return null;
            }
            if (user.UserName.IsNullOrEmpty()) return null;
            existingUser.UserName = user.UserName;
            var result = await _userManager.UpdateAsync(existingUser);
            return result;
        }

        public List<Artist> GetArtists(User user)
        {
            var artists = _context.FollowedArtists
                .Where(fs => fs.UserId == user.Id)
                .Select(fs => fs.ArtistId)
                .Distinct() // Add distinct to avoid duplicates
                .ToList();

            user.FollowedArtists.Clear(); // Clear the existing favorite songs collection

            foreach (var artistId in artists)
            {
                var artist = _context.Artists.FirstOrDefault(s => s.Id == artistId);
                if (artist != null)
                {
                    //song.Artist = _context.Artists.FirstOrDefault(artist => artist.Id == song.ArtistId);
                    user.FollowedArtists.Add(artist);
                }
            }

            return user.FollowedArtists;
        }
        public List<Song> GetSongs(User user)
        {
            var songs = _context.FavoriteSongs
                .Where(fs => fs.UserId == user.Id)
                .Select(fs => fs.SongId)
                .Distinct() // Add distinct to avoid duplicates
                .ToList();

            user.FavoriteSongs.Clear(); // Clear the existing favorite songs collection

            foreach (var songId in songs)
            {
                var song = _context.Songs.FirstOrDefault(s => s.Id == songId);
                if (song != null)
                {
                    song.Artist = _context.Artists.FirstOrDefault(artist => artist.Id == song.ArtistId);
                    user.FavoriteSongs.Add(song);
                }
            }

            return user.FavoriteSongs;
        }
    }
}
