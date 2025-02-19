using AutoMapper;
using Cinema.DataAccess.Models;
using Cinema.Web.Models;

namespace Cinema.Web
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<Movie, MovieViewModel>(MemberList.Destination);
        }
    }
}
