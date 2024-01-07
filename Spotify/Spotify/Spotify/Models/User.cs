using System.ComponentModel.DataAnnotations;

namespace Spotify.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ProfilePhoto { get; set; }
        public List<Song> FavoriteSongs { get; set; } // List of favorite songs for the user
        public List<Artist> FollowedArtists { get; set; } // List of artists the user is following

        // Constructor
        public User()
        {
            FavoriteSongs = new List<Song>();
            FollowedArtists = new List<Artist>();
        }
    }
}
