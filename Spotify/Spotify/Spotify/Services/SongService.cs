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

        //public async Task<DownloadMusic> DownloadMusic(string songId)
        //{
        //    // Path to the MP3 file on the server
        //    var song = await _context.Songs.FindAsync(songId);
        //    var filePath = song.AudioURL;

        //    // Read the file bytes
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

        //    // Set the content type and file name for the response
        //    var content = new DownloadMusic
        //    {
        //        contentType = "audio/mpeg",
        //        fileName = song.Title,
        //        fileBytes = fileBytes,
        //    };

        //    return content;
        //}

        public async Task<int> FavoriteSongs(int songId, User user)
        {
            // Find the song by ID
            var song = _context.Songs.FirstOrDefault(s => s.Id == songId);

            var favoriteSong = _context.FavoriteSongs
                .FirstOrDefault(s => s.UserId == user.Id && s.SongId == song.Id);
            if (song != null)
            {
                if (favoriteSong == null)
                {
                    var favorite = new FavoriteSongs
                    {
                        SongId = song.Id, 
                        UserId = user.Id
                    };
                    _context.FavoriteSongs.Add(favorite);
                    await _context.SaveChangesAsync();
                }
                else if (favoriteSong != null)
                {
                    _context.FavoriteSongs.Remove(favoriteSong);
                    await _context.SaveChangesAsync();
                }
                return 1;
            }
            return 0;
        }
    }
}
