namespace Cinema.Shared.Models
{
    public class ReservationResponseDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required string Email { get; init; }
        public required string Phone { get; init; }
        public DateTime CreatedAt { get; init; }
        public string? Comment { get; init; }
        public required List<SeatResponseDto> Seats { get; init; }
        public required ScreeningResponseDto Screening { get; init; }
    }
}
