using Cinema.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

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
            FormattableString sqlQuery =
                $"""
                SELECT Id, Title, Year, Director, Synopsis, Length, Image, CreatedAt, DeletedAt
                FROM Movies
                WHERE DeletedAt IS NULL
                ORDER BY CreatedAt DESC
                """;

            sqlQuery = count > 0 ? FormattableStringFactory.Create(sqlQuery.Format + $"TOP {count}") : sqlQuery;

            return await _context.Movies
                .FromSql(sqlQuery)
                .ToListAsync();

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
