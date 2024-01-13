using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Spotify.Models.context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        //public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) 
        { 
            
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Song>()
        //            .HasOne(c => c.Artist)
        //            .WithMany()
        //            .IsRequired(false);

        //    modelBuilder.Entity<Artist>()
        //        .HasRequired(s => s.Song)
        //        .WithMany()
        //        .WillCascadeOnDelete(false);
        //}
    }
}
