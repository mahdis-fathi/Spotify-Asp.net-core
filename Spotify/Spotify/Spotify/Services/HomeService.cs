using Spotify.Models;
using Spotify.Models.context;

namespace Spotify.Services
{
    public class HomeService : IHome
    {
        private readonly AppDbContext _context;
        public HomeService(AppDbContext context)
        {
            _context = context;
        }
        public List<Song> Search(string searchTerm)
        {
            var songs = GetSongs();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                songs = songs.Where(s =>
                    s.Title.ToLower().Contains(searchTerm) ||
                    s.Artist.Name.ToLower().Contains(searchTerm) 
                ).ToList();
            }
            return songs.ToList();
        } 
        private List<Song> GetSongs()
        {
           return _context.Songs.ToList();
        }
    }
}
