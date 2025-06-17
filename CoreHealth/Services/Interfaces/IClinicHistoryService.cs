using CoreHealth.DTOs;

namespace CoreHealth.Services.Interfaces
{
    public interface IClinicHistoryService
    {
        Task<List<ClinicHistoryDTO>> GetAllAsync();
        Task<ClinicHistoryDTO> GetByIdAsync(int id);
        Task AddAsync(ClinicHistoryDTO clinicHistoryDTO);
        Task UpdateAsync(ClinicHistoryDTO clinicHistoryDTO);
        Task DeleteAsync(int id);
    }
}
