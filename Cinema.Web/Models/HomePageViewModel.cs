namespace Cinema.Web.Models
{
    public class HomePageViewModel
    {
        public required List<MovieViewModel> LatestMovies { get; set; }
        public required List<ScreeningViewModel> TodayScreenings { get; set; }
    }
}
