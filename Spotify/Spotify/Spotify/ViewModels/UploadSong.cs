using System.ComponentModel.DataAnnotations;

namespace Spotify.ViewModels
{
    public class UploadSong
    {
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string? AudioURL { get; set; }
        public string? CoverURL { get; set; }
        public string Duration { get; set; }
        //public List<Artist> Artists { get; set; }
    }
}
