using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Shared.Models
{
    public class ScreeningResponseDto
    {
        public int Id { get; init; }
        public MovieResponseDto Movie { get; init; }
        public RoomResponseDto Room { get; init; }
        public DateTime StartsAt { get; init; }
        public decimal Price { get; init; }
    }
}
