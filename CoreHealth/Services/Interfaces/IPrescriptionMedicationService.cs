using CoreHealth.DTOs;

namespace CoreHealth.Services.Interfaces
{
    public interface IPrescriptionMedicationService
    {
        Task<List<PrescriptionMedicationDTO>> GetAllAsync();
        Task<PrescriptionMedicationDTO> GetByIdAsync(int id);
        Task AddAsync(PrescriptionMedicationDTO prescriptionMedicationDTO);
        Task UpdateAsync(PrescriptionMedicationDTO prescriptionMedicationDTO);
        Task DeleteAsync(int id);
    }
}
