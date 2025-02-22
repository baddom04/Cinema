using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Cinema.DataAccess.Exceptions;

namespace Cinema.DataAccess.Services
{
    public class MoviesSqlService : IMoviesService
    {
        private CinemaDbContext _context;
        public MoviesSqlService(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<Movie>> GetLatestMoviesAsync(int? count = null)
        {
            if (count is null)
            {
                return await _context.Movies.FromSql(
                    $"""
                 SELECT
                 Id, Title, Year, Director, Synopsis, Length, Image, CreatedAt, DeletedAt
                 FROM Movies
                 WHERE DeletedAt is null
                 ORDER BY CreatedAt DESC
                 """
                ).ToListAsync();
            }

            return await _context.Movies.FromSql(
                $"""
             SELECT TOP ({count.Value})
             Id, Title, Year, Director, Synopsis, Length, Image, CreatedAt, DeletedAt
             FROM Movies
             WHERE DeletedAt is null
             ORDER BY CreatedAt DESC
             """
            ).ToListAsync();

            //return count is null ? await _context.Movies.OrderBy(x => x.CreatedAt).ToListAsync() : await _context.Movies.OrderBy(x => x.CreatedAt).Take(count.Value).ToListAsync();
        }
        public async Task<Movie> GetByIdAsync(int id)
        {
            FormattableString sqlQuery =
                $"""
                SELECT *
                FROM movies
                WHERE Id = {id}
                AND DeletedAt is null
                """;

            return await _context.Movies
                .FromSql(sqlQuery)
                .FirstOrDefaultAsync() ?? throw new EntityNotFoundException();

            //  To Linq statement's end: ToQueryString() to see the SQL string

            //return await _context.Movies.FindAsync(id) ?? throw new EntityNotFoundException();
        }
    }
}
