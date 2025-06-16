using CoreHealth.DTOs;

namespace CoreHealth.Services
{
    public interface IDoctorService
    {
        Task<List<DoctorDTO>> GetAllAsync();
        Task<DoctorDTO> GetByIdAsync(int id);
        Task AddAsync(DoctorDTO doctorDTO);
        Task UpdateAsync(DoctorDTO doctorDTO);
        Task DeleteAsync(int id);
    }
}
