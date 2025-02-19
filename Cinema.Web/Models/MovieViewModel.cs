namespace Cinema.Web.Models
{
    public class MovieViewModel
    {
        public Int32 MyProperty { get; set; }
        public string Title { get; set; }
        public Int32 Year { get; set; }
        public string Director { get; set; }
        public string Synopsis { get; set; }
        public Int32 Length { get; set; }
        public byte[] Image { get; set; }
    }
}
