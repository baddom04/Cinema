using AutoMapper;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services.Interfaces;
using Cinema.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController(IReservationService reservationService, IMapper mapper) : ControllerBase
    {
        private readonly IReservationService _reservationService = reservationService;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationRequestDto reservationRequestDto)
        {
            Reservation reservation = _mapper.Map<Reservation>(reservationRequestDto);
            await _reservationService.AddAsync(reservationRequestDto.ScreeningId, reservation);

            var reservationResponseDto = _mapper.Map<ReservationResponseDto>(reservation);

            return CreatedAtAction(nameof(CreateReservation), new { id = reservationResponseDto.Id }, reservationRequestDto);
        }
    }
}
