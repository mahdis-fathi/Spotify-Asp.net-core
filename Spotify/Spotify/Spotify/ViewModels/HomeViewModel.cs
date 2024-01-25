using Spotify.Models;

namespace Spotify.ViewModels
{
    public class HomeViewModel
    {
        public List<Song> Songs { get; set; }
        public List<Song> Favorites { get; set; }
    }
}
