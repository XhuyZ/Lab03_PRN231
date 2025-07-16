using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IOrchidService
    {
        Task<IEnumerable<OrchidDTO>> GetAllAsync();
        Task<OrchidDTO?> GetByIdAsync(int id);
        Task<IEnumerable<OrchidDTO>> SearchAsync(string keyword);
        Task<OrchidDTO> CreateAsync(OrchidDTO orchidDto);
        Task<OrchidDTO> UpdateAsync(OrchidDTO orchidDto);
        Task<bool> DeleteAsync(int id);
    }
}

