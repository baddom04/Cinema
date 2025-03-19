using Cinema.DataAccess.Exceptions;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DataAccess.Services
{
    public class ScreeningService(CinemaDbContext context) : IScreeningService
    {
        private readonly CinemaDbContext _context = context;
        public async Task<IReadOnlyCollection<Screening>> GetAllAsync(int? movieId = null, int? roomId = null, DateTime? from = null, DateTime? until = null)
        {
            return await _context.Screenings
                .Where(src => !movieId.HasValue || src.MovieId == movieId.Value)
                .Where(src => !roomId.HasValue || src.RoomId == roomId.Value)
                .Where(src => !from.HasValue || src.StartsAt > from)
                .Where(src => !until.HasValue || src.StartsAt < until)
                .ToListAsync();
        }

        public async Task<Screening> GetByIdAsync(int id)
        {
            return await _context.Screenings.FirstOrDefaultAsync(x => x.Id == id) ?? throw new EntityNotFoundException();
        }

        public async Task<IReadOnlyCollection<Screening>> GetForDateAsync(DateTime date)
        {
            return await _context.Screenings
                .Where(scr => scr.StartsAt.Date.Equals(date.Date))
                .ToListAsync();
        }

        public async Task<List<Seat>> GetSeatsByScreeningAsync(int id)
        {
            return await _context.Seats
                .Where(s => s.ScreeningId == id)
                .ToListAsync();
        }
    }
}
