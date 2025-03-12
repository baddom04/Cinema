using AutoMapper;
using Cinema.DataAccess.Services.Interfaces;
using Cinema.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController(IMapper mapper, IRoomService roomService) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IRoomService _roomService = roomService;

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            IEnumerable<RoomResponseDto> rooms = _mapper.Map<IEnumerable<RoomResponseDto>>(await _roomService.GetAllAsync());
            return Ok(rooms);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRoomById([FromRoute] int id)
        {
            RoomResponseDto room = _mapper.Map<RoomResponseDto>(await _roomService.GetByIdAsync(id));
            return Ok(room);    
        }
    }
}
