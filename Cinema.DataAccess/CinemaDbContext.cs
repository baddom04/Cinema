using Cinema.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DataAccess
{
    public class CinemaDbContext(DbContextOptions<CinemaDbContext> options) : DbContext(options)
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Reservation)
                .WithMany(r => r.Seats)
                .HasForeignKey(s => s.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
