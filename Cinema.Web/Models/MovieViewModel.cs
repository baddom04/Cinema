namespace Cinema.Web.Models
{
    public class MovieViewModel
    {
        public Int32 Id { get; set; }
        public required string Title { get; set; }
        public Int32 Year { get; set; }
        public required string Director { get; set; }
        public required string Synopsis { get; set; }
        public Int32 Length { get; set; }
        public required byte[] Image { get; set; }
    }
}
