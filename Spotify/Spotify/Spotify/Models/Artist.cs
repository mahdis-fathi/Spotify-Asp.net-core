namespace Spotify.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePhoto { get; set; }
        public List<User> Followers { get; set; } // List of users who are following this artist

        // Constructor
        public Artist()
        {
            Followers = new List<User>();
        }
    }
}
