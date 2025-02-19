using System.Diagnostics;
using AutoMapper;
using Cinema.DataAccess;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IMoviesService movieService, IMapper mapper, IConfiguration configuration) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        private readonly IMoviesService _moviesService = movieService;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;
        private readonly int _movieCount = 5;

        public async Task<IActionResult> Index()
        {
            IReadOnlyCollection<Movie> lastestMovies = await _moviesService.GetLatestMoviesAsync(int.Parse(_configuration["NewMovieCount"]!));
            List<MovieViewModel> lastestMoviesViewModels = _mapper.Map<List<MovieViewModel>>(lastestMovies);

            var homePageViewModel = new HomePageViewModel()
            {
                LatestMovies = lastestMoviesViewModels
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
