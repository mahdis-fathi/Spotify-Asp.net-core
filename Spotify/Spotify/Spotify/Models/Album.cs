namespace Spotify.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string? CoverPhotoURL { get; set; }

        public List<Song> Songs { get; set; } // List of songs in the album

        // Constructor
        public Album()
        {
            Songs = new List<Song>();
        }
    }
}
