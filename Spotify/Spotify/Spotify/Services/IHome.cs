using Spotify.Models;

namespace Spotify.Services
{
    public interface IHome
    {
        List<Song> Search(string searchTerm);
    }
}
