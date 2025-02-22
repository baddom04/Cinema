using System.ComponentModel.DataAnnotations;

namespace Cinema.Web.Models
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "The name field is required.")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "The phone number field is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public required string Phone { get; init; }

        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "The email address is not in the correct format.")]
        [DataType(DataType.EmailAddress)] // data type for validation
        public required string Email { get; init; }

        [StringLength(160, ErrorMessage = "The maximum length of the comment can be 160 characters.")]
        public string? Comment { get; init; }

        public long ScreeningId { get; init; }

        public required List<SeatViewModel> Seats { get; init; }
    }
}
