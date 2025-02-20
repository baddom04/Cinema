using AutoMapper;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services.Interfaces;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cinema.Web.Controllers
{
    public class HomeController(
        ILogger<HomeController> logger, 
        IMoviesService movieService,
        IScreeningService screeningService,
        IMapper mapper, 
        IConfiguration configuration
        ) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        private readonly IMoviesService _moviesService = movieService;
        private readonly IScreeningService _screeningService = screeningService;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;

        public async Task<IActionResult> Index()
        {
            var homePageViewModel = new HomePageViewModel()
            {
                LatestMovies = _mapper.Map<List<MovieViewModel>>(await _moviesService.GetLatestMoviesAsync(int.Parse(_configuration["NewMovieCount"]!))),
                TodayScreenings = _mapper.Map<List<ScreeningViewModel>>(await _screeningService.GetForDateAsync(DateTime.Now)),
            };

            return View(homePageViewModel);
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
