namespace Spotify.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string AudioURL { get; set; }
        public string? CoverURL { get; set; }
        public string Duration { get; set; }
    }
}
