using Cinema.DataAccess.Models;

namespace Cinema.DataAccess.Services.Interfaces
{
    public interface IReservationService
    {
        Task AddAsync(long screenId, Reservation reservation);
    }
}
