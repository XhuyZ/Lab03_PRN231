using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IOrchidRepository
    {
        Task<IEnumerable<Orchid>> GetAllAsync();
        Task<Orchid?> GetByIdAsync(int id);
        Task<IEnumerable<Orchid>> SearchAsync(string keyword);
        Task AddAsync(Orchid orchid);
        Task UpdateAsync(Orchid orchid);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}

