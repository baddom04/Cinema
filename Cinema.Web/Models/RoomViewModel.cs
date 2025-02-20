namespace Cinema.Web.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
    }
}
