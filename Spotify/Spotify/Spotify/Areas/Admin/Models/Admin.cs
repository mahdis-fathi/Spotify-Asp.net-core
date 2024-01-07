namespace Spotify.Areas.Admin.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        // Additional properties specific to admin users, such as password, role, etc.
    }
}
