using CoreHealth.DTOs;

namespace CoreHealth.Services.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceDTO>> GetAllAsync();
        Task<ServiceDTO> GetByIdAsync(int id);
        Task AddAsync(ServiceDTO serviceDTO);
        Task UpdateAsync(ServiceDTO serviceDTO);
        Task DeleteAsync(int id);
    }
}
