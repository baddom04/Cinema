using Cinema.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DataAccess
{
    public class CinemaDbContext(DbContextOptions<CinemaDbContext> options) : DbContext(options)
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Screening> Screenings { get; set; }
    }
}
