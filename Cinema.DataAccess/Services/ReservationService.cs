using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services.Interfaces;

namespace Cinema.DataAccess.Services
{
    public class ReservationService : IReservationService
    {
        public async Task AddAsync(long screenId, Reservation reservation)
        {
            // TODO: implement
            await Task.Delay(100);
        }
    }
}
