namespace Spotify.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public string AudioURL { get; set; }
    }
}
