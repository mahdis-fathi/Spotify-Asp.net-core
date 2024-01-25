using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spotify.Models;
using Spotify.Models.context;

namespace Spotify.Services
{
    public class ArtistService : IArtist
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public ArtistService(AppDbContext dbContext, UserManager<User> userManager)
        {
            _context = dbContext;
            _userManager = userManager;
        }
        public async Task<int> FollowArtist(int artistId, User user)
        {
            var artist = _context.Artists.Find(artistId);
            if (artist == null) return 0;
            var followedArtist = _context.FollowedArtists
                .FirstOrDefault(s => s.UserId == user.Id && s.ArtistId == artist.Id);
            if (followedArtist == null)
            {
                var followed = new FollowedArtist
                {
                    ArtistId = artist.Id,
                    UserId = user.Id
                };
                _context.FollowedArtists.Add(followed);
                await _context.SaveChangesAsync();
                return 1;
            }
            else if (followedArtist != null)
            {
                _context.FollowedArtists.Remove(followedArtist);
                await _context.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<List<User>> Follwers(Artist artist)
        {
            var followers = _context.FollowedArtists
                .Where(fs => fs.ArtistId == artist.Id)
                .Select(fs => fs.UserId)
                .Distinct() // Add distinct to avoid duplicates
                .ToList();

            artist.Followers.Clear(); // Clear the existing favorite songs collection

            foreach (var follow in followers)
            {
                var user = await _userManager.FindByIdAsync(follow);
                if (user != null)
                {
                    //song.Artist = _context.Artists.FirstOrDefault(artist => artist.Id == song.ArtistId);
                    artist.Followers.Add(user);
                }
            }

            return artist.Followers;
        }

        public async Task<List<Song>> GetSongs(Artist artist)
        {
            var songs = await _context.Songs
    .Where(fs => fs.ArtistId == artist.Id)
    .Distinct()
    .ToListAsync();

            artist.Songs.Clear();
            artist.Songs.AddRange(songs);

            return artist.Songs;
        }

        //public async Task<int> UnfollowArtist(int artistId, User user)
        //{
        //    var artist = _context.Artists.Find(artistId);
        //    if (artist == null) return 0;
        //    var followedArtist = _context.FollowedArtists
        //        .FirstOrDefault(s => s.UserId == user.Id && s.ArtistId == artist.Id);
        //    if (followedArtist != null)
        //    {
        //        _context.FollowedArtists.Remove(followedArtist);
        //        await _context.SaveChangesAsync();
        //        return 1;
        //    }
        //    return 0;
        //}
    }
}
