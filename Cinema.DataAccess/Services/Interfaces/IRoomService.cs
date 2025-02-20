using Cinema.DataAccess.Models;

namespace Cinema.DataAccess.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IReadOnlyCollection<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
    }
}
