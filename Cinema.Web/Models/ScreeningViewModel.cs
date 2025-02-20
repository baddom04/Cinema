namespace Cinema.Web.Models
{
    public class ScreeningViewModel
    {
        public int Id { get; set; }
        public required MovieViewModel Movie { get; set; }
        public required RoomViewModel Room { get; set; }
        public DateTime StartsAt { get; set; }
        public decimal Price { get; set; }
    }
}
