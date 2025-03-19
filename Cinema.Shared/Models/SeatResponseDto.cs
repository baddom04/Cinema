namespace Cinema.Shared.Models
{
    public class SeatResponseDto
    {
        public int Id { get; init; }
        public int Row { get; init; }
        public int Column { get; init; }
        public SeatStatusDto Status { get; init; }
    }
}