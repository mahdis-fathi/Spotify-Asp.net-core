using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Spotify.Models.context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<FavoriteSongs> FavoriteSongs { get; set; }
        public DbSet<FollowedArtist> FollowedArtists { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) 
        { 
            
        }
    }
}
