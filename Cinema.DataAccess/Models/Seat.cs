using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DataAccess.Models
{
    public class Seat
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Screening))]
        public int ScreeningId { get; set; }

        public virtual Screening Screening { get; set; } = null!;

        public SeatStatus Status { get; set; }

        [ForeignKey(nameof(Reservation))]
        public int? ReservationId { get; set; }

        public virtual Reservation? Reservation { get; set; }

        [Owned]
        public record SeatPosition(int Row, int Column);
    }
    
}
