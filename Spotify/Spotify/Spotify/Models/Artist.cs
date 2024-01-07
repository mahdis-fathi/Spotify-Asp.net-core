namespace Spotify.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Add additional properties specific to artists, such as bio or profile picture

        public List<User> Followers { get; set; } // List of users who are following this artist

        // Constructor
        public Artist()
        {
            Followers = new List<User>();
        }
    }
}
