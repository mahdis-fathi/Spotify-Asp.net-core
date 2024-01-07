using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Spotify.Areas.Admin.Models;

namespace Spotify.Models.context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) 
        { 
            
        }
        //public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        //public DbSet<Admin> Admins { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
    }
}
