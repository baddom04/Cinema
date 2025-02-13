using System.ComponentModel.DataAnnotations;

namespace Cinema.DataAccess.Models
{
    public class Movie
    {
        [Key]                       public int Id { get; set; }
        [MaxLength(255)] [Required] public required string Title { get; set; }
                                    public int Year { get; set; }
        [Required]                  public required string Director { get; set; }
        [Required]                  public required string Synopsis { get; set; }
                                    public int Length { get; set; }
        [Required]                  public required byte[] Image { get; set; }
                                    public DateTime CreatedAt { get; set; }
                                    public DateTime? DeletedAt { get; set; }
    }
}
