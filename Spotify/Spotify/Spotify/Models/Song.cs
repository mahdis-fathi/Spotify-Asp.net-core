namespace Spotify.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        // Add additional properties such as duration, audio file URL, etc.
    }
}
