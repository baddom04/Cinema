namespace Cinema.DataAccess.Models
{
    public class Room
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual ICollection<Screening> Screenings { get; set; } = [];
    }
}
