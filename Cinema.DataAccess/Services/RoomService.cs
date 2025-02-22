using Cinema.DataAccess.Exceptions;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DataAccess.Services
{
    public class RoomService(CinemaDbContext context) : IRoomService
    {
        private readonly CinemaDbContext _context = context;
        public async Task<IReadOnlyCollection<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id) ?? throw new EntityNotFoundException();
        }
    }
}
