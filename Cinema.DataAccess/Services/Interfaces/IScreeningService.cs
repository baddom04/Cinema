using Cinema.DataAccess.Models;

namespace Cinema.DataAccess.Services.Interfaces
{
    public interface IScreeningService
    {
        Task<IReadOnlyCollection<Screening>> GetForDateAsync(DateTime date);
        Task<Screening> GetByIdAsync(int id);
        Task<IReadOnlyCollection<Screening>> GetAllAsync(int? movieId = null, int? roomId = null, DateTime? from = null, DateTime? until = null);
    }
}
