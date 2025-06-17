using CoreHealth.DTOs;

namespace CoreHealth.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentDTO>> GetAllAsync();
        Task<AppointmentDTO> GetByIdAsync(int id);
        Task AddAsync(AppointmentDTO appointmentDTO);
        Task UpdateAsync(AppointmentDTO appointmentDTO);
        Task DeleteAsync(int id);
    }
}
