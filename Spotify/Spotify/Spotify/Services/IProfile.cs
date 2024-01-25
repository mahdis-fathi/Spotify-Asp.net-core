using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Models;

namespace Spotify.Services
{
    public interface IProfile
    {
        Task<User> GetAsync(string id);
        Task<IdentityResult> EditUser(User user);
        List<Artist> GetArtists(User user);
        List<Song> GetSongs(User user);
    }
}
