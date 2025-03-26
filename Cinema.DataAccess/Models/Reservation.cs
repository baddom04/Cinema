using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DataAccess.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public required string Name { get; set; }
        [MaxLength(255)]
        public required string Email { get; set; }
        [MaxLength(15)]
        public required string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Comment { get; set; }
        public virtual ICollection<Seat> Seats { get; set; } = [];

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
