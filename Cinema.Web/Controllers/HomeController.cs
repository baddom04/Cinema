using System.Diagnostics;
using Cinema.DataAccess;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //CinemaDbContext context = new(/* ... */);

            //context.Movies
            //    .Where(m => m.Year > 2020 && m.Length < 120)
            //    .OrderBy(m => m.Title)
            //    .ToList();

            return View();
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
