using Spotify.Models;

namespace Spotify.Services
{
    public interface IArtist
    {
        Task<int> FollowArtist(string artistId, User user);
    }
}
