namespace Cinema.Shared.Models
{
    public class MovieResponseDto
    {
        public int Id { get; init; }
        public required string Title { get; init; }
        public int Year { get; init; }
        public required string Director { get; init; }
        public required string Synopsis { get; init; }
        public int Length { get; init; }
        public required byte[] Image { get; init; }
    }
}
