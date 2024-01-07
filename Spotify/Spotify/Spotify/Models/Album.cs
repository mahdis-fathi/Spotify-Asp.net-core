namespace Spotify.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        // Add additional properties such as release year or cover image URL

        public List<Song> Songs { get; set; } // List of songs in the album

        // Constructor
        public Album()
        {
            Songs = new List<Song>();
        }
    }
}
