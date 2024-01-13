using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Spotify.Models
{
    public class User : IdentityUser
    {
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
