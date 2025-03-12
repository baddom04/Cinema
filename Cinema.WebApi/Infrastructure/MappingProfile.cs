using AutoMapper;
using Cinema.DataAccess.Models;
using Cinema.Shared.Models;

namespace Cinema.WebApi.Infrastructure;

public class MappingProfile : Profile
{
	public MappingProfile()
	{	
		CreateMap<Movie, MovieResponseDto>(MemberList.Destination);
		CreateMap<Room, RoomResponseDto>(MemberList.Destination);
		CreateMap<Screening, ScreeningResponseDto>(MemberList.Destination);
	}
}
