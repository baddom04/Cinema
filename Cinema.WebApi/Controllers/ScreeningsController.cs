using AutoMapper;
using Cinema.DataAccess.Services.Interfaces;
using Cinema.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningsController(IMapper mapper, IScreeningService screeningService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IScreeningService _screeningService = screeningService;

        [HttpGet]
        public async Task<IActionResult> GetScreenings()
        {
            IEnumerable<ScreeningResponseDto> screenings = _mapper.Map<IEnumerable<ScreeningResponseDto>>(await _screeningService.GetAllAsync());
            return Ok(screenings);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetScreeningById([FromRoute] int id)
        {
            ScreeningResponseDto screening = _mapper.Map<ScreeningResponseDto>(await _screeningService.GetByIdAsync(id));
            return Ok(screening);
        }
    }
}
