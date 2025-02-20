using AutoMapper;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services.Interfaces;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers
{
    public class MovieController(IMoviesService moviesService, IScreeningService screeningService, IMapper mapper) : Controller
    {
        private readonly IMoviesService _moviesService = moviesService;
        private readonly IScreeningService _screeningService = screeningService;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<MovieViewModel>>(await _moviesService.GetLatestMoviesAsync()));
        }

        public async Task<IActionResult> Details(Int32 movieId)
        {
            return View(new MovieDetailViewModel()
            {
                Movie = _mapper.Map<MovieViewModel>(await _moviesService.GetByIdAsync(movieId)),
                Screenings = _mapper.Map<List<ScreeningViewModel>>(await _screeningService.GetAllAsync(movieId: movieId, from: DateTime.Now))
            });
        }
    }
}
