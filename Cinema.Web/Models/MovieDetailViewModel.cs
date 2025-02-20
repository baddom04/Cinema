namespace Cinema.Web.Models
{
    public class MovieDetailViewModel
    {
        public required MovieViewModel Movie { get; set; }
        public required List<ScreeningViewModel> Screenings { get; set; }
    }
}
