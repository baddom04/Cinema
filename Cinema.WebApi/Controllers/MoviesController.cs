using AutoMapper;
using Cinema.Shared.Models;
using Cinema.DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController(IMapper mapper, IMoviesService moviesService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IMoviesService _moviesService = moviesService;

        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] int? count = null)
        {
            IEnumerable<MovieResponseDto> movies = _mapper.Map<IEnumerable<MovieResponseDto>>(await _moviesService.GetLatestMoviesAsync(count));
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMovieById([FromRoute] int id)
        {
            MovieResponseDto movie = _mapper.Map<MovieResponseDto>(await _moviesService.GetByIdAsync(id));
            return Ok(movie);
        }
    }
}
