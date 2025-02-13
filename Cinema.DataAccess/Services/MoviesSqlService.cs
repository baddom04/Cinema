using Cinema.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Cinema.DataAccess.Services
{
    public class MoviesSqlService : IMoviesService
    {
        private static CinemaDbContext _context;
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

            sqlQuery = count > 0 ? FormattableStringFactory.Create(sqlQuery.Format + $"LIMIT {count}") : sqlQuery;

            return await _context.Movies
                .FromSql(sqlQuery)
                .ToListAsync();
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
        }
    }
}
