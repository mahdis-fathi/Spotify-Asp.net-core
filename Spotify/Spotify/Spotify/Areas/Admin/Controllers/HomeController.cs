using Microsoft.AspNetCore.Authorization;

namespace Spotify.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController
    {
    }
}
