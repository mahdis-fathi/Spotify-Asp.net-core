using Spotify.Models;

namespace Spotify.Classes
{
    public class AddArtists
    {
        public static List<Artist> AddArtist()
        {
            var artists = new List<Artist>();
            artists.Add(new Artist() { Name = "taylor swift"});
            artists.Add(new Artist() { Name = "halsey"});
            artists.Add(new Artist() { Name = "troy sivan"});
            artists.Add(new Artist() { Name = "lady gaga"});
            artists.Add(new Artist() { Name = "maneskin"});
            artists.Add(new Artist() { Name = "aurora" });
            return artists;
        }
    }
}
