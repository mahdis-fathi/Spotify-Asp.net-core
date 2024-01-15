using Spotify.Classes;
using Spotify.Models;

namespace Spotify.Services
{
    public interface ISong
    {
        Task<int> FavoriteSongs(string songId, User user);
        Task<DownloadMusic> DownloadMusic(string songId);
    }
}
