using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spotify.Classes;
using Spotify.Models;
using Spotify.Models.context;

namespace Spotify.Services
{
    public class SongService : ISong
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        public SongService(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<DownloadMusic> DownloadMusic(string songId)
        {
            // Path to the MP3 file on the server
            var song = await _context.Songs.FindAsync(songId);
            var filePath = song.AudioURL;

            // Read the file bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Set the content type and file name for the response
            var content = new DownloadMusic
            {
                contentType = "audio/mpeg",
                fileName = song.Title,
                fileBytes = fileBytes,
            };

            return content;
        }

        public async Task<int> FavoriteSongs(string songId, User user)
        {
            // Find the song by ID
            var song = _context.Songs.Find(songId);

            if (song != null)
            {
                if (!user.FavoriteSongs.Contains(song))
                {
                    user.FavoriteSongs.Add(song);
                    await _userManager.UpdateAsync(user);
                }
                else if (user.FavoriteSongs.Contains(song))
                {
                    user.FavoriteSongs.Remove(song);
                    await _userManager.UpdateAsync(user);
                }
                return 1;
            }
            return 0;
        }
    }
}
