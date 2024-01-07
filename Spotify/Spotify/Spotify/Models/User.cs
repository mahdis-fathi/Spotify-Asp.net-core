namespace Spotify.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        // Add additional properties such as password, profile picture, etc.

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
