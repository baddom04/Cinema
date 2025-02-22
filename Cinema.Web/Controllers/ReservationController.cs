using AutoMapper;
using Cinema.DataAccess.Config;
using Cinema.DataAccess.Exceptions;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services.Interfaces;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cinema.Web.Controllers
{
    public class ReservationController(IReservationService reservationService, IMapper mapper, IScreeningService screeningService, IOptions<ReservationSettings> reservationSettings) : Controller
    {
        private readonly IReservationService _reservationService = reservationService;
        private readonly IMapper _mapper = mapper;
        private readonly IScreeningService _screeningService = screeningService;
        private readonly ReservationSettings _reservationSettings = reservationSettings.Value;
        [HttpGet]
        public async Task<IActionResult> Index(int screeningId)
        {
            try
            {
                Screening screening = await _screeningService.GetByIdAsync(screeningId);
                ScreeningViewModel screeningViewModel = _mapper.Map<ScreeningViewModel>(screening);

                int rows = screening.Room.Rows;
                int columns = screening.Room.Columns;

                List<SeatViewModel> seats = [];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        seats.Add(new SeatViewModel
                        {
                            Row = i,
                            Column = j,
                            Status = screening.Seats.Any(s => s.Position.Column == j && s.Position.Row == i)
                            ? SeatViewModelStatus.Reserved
                            : SeatViewModelStatus.Free,
                        });
                    }
                }

                return View(new ReservationPageViewModel()
                {
                    ScreeningViewModel = screeningViewModel,
                    SeatViewModels = seats,
                    MaximumNumberOfSeats = _reservationSettings.MaximumNumberOfSeats
                });
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Reserve([FromBody] ReservationViewModel reservationViewModel)
        {
            Reservation reservation = _mapper.Map<Reservation>(reservationViewModel);
            await _reservationService.AddAsync(reservationViewModel.ScreeningId, reservation);
            return View();
        }
    }
}
