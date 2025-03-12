namespace Cinema.Shared.Models
{
    public class RoomResponseDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public int Rows { get; init; }
        public int Columns { get; init; }
    }
}
