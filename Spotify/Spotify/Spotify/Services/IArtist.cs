using Spotify.Models;

namespace Spotify.Services
{
    public interface IArtist
    {
        Task<int> FollowArtist(int artistId, User user);
        //Task<int> UnfollowArtist(int artistId, User user);
        Task<List<User>> Follwers(Artist artist);
        Task<List<Song>> GetSongs(Artist artist);
    }
}
