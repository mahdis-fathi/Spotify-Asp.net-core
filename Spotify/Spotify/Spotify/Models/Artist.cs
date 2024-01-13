namespace Spotify.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePhoto { get; set; }
        public List<User> Followers { get; set; } // List of users who are following this artist
        public List<Song> Songs { get; set; }
        public List<Album> Albums { get; set; }

        // Constructor
        public Artist()
        {
            Followers = new List<User>();
            Songs = new List<Song>();
            Albums = new List<Album>();
        }
    }
}
