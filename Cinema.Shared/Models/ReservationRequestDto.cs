using System.ComponentModel.DataAnnotations;

namespace Cinema.Shared.Models
{
    public class ReservationRequestDto
    {
        [StringLength(255, ErrorMessage = "Name is too long.")]
        public required string Name { get; init; }
        public required string Phone { get; init; }
        public required string Email { get; init; }
        public string? Comment { get; init; }
        public long ScreeningId { get; init; }
        public required List<SeatRequestDto> Seats { get; init; }
    }
}
