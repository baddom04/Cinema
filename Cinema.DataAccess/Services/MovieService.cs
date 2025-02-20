using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DataAccess.Services
{
    public class MovieService(CinemaDbContext context) : IMoviesService
    {
        private readonly CinemaDbContext _context = context;
        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new EntityNotFoundException();
        }

        public async Task<IReadOnlyCollection<Movie>> GetLatestMoviesAsync(int? count = null)
        {
            return await _context.Movies
                .Where(x => !x.DeletedAt.HasValue)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
