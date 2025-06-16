using CoreHealth.DTOs;

namespace CoreHealth.Services
{
    public interface IClinicService
    {
        Task<List<ClinicDTO>> GetAllAsync();
        Task<ClinicDTO> GetByIdAsync(int id);
        Task AddAsync(ClinicDTO clinicDTO);
        Task UpdateAsync(ClinicDTO clinicDTO);
        Task DeleteAsync(int id);
    }
}
