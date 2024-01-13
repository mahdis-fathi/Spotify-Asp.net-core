using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Spotify.Models;
using Spotify.Services;
using System.Diagnostics;

namespace Spotify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHome _home;

        public HomeController(ILogger<HomeController> logger, IHome home)
        {
            _logger = logger;
            _home = home;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search(string searchTerm)
        {
            var songs = _home.Search(searchTerm);
            return View(songs);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}