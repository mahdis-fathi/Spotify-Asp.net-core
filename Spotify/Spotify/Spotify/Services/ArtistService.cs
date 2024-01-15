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
        public async Task<int> FollowArtist(string artistId, User user)
        {
            var artist = _context.Artists.Find(artistId);

            if (artist != null)
            {
                if (!user.FollowedArtists.Contains(artist))
                {
                    user.FollowedArtists.Add(artist);
                    await _userManager.UpdateAsync(user);
                }
                else if (user.FollowedArtists.Contains(artist))
                {
                    user.FollowedArtists.Remove(artist);
                    await _userManager.UpdateAsync(user);
                }
                return 1;
            }
            return 0;
        }
    }
}
