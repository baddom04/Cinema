using System.ComponentModel.DataAnnotations;

namespace Cinema.DataAccess.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(255)] public required string Title { get; set; }
        public int Year { get; set; }
        public required string Director { get; set; }
        public required string Synopsis { get; set; }
        public int Length { get; set; }
        public required byte[] Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual ICollection<Screening> Screenings { get; set; } = [];
    }
}
