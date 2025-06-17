using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using CoreHealth.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreHealth.Services.Implements
{
    public class PrescriptionMedicationService : IPrescriptionMedicationService
    {
        private readonly ApplicationDbContext _context;
        public PrescriptionMedicationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<PrescriptionMedicationDTO>> GetAllAsync()
        {
            var prescriptionsMedication = await _context.PrescriptionMedication
            .Select(static pm => new PrescriptionMedicationDTO
            {
                Id = pm.Id,
                PrescriptionId = pm.PrescriptionId,
                MedicationId = pm.MedicationId,
                MedicationName = pm.Medication.Name,
                Dosage = pm.Dosage,
                Frequency = pm.Frequency,
                Duration = pm.Duration
            })
            .ToListAsync();

            return prescriptionsMedication;
        }
        public async Task<PrescriptionMedicationDTO> GetByIdAsync(int id)
        {
            var prescriptionMedication = await _context.PrescriptionMedication
                .DefaultIfEmpty()
                .Select(pm => new PrescriptionMedicationDTO
                {
                    Id = pm.Id,
                    PrescriptionId = pm.PrescriptionId,
                    MedicationId = pm.MedicationId,
                    MedicationName = pm.Medication.Name,
                    Dosage = pm.Dosage,
                    Frequency = pm.Frequency,
                    Duration = pm.Duration
                })
                .FirstOrDefaultAsync(ch => ch.Id == id);
            if (prescriptionMedication == null)
                throw new ApplicationException("No hay medicamentos asignados");
            return prescriptionMedication;
        }

        public async Task AddAsync(PrescriptionMedicationDTO prescriptionMedicationDTO)
        {
            var prescriptionMedication = new PrescriptionMedication
            {
                PrescriptionId = prescriptionMedicationDTO.PrescriptionId,
                MedicationId = prescriptionMedicationDTO.MedicationId,
                Dosage = prescriptionMedicationDTO.Dosage,
                Frequency = prescriptionMedicationDTO.Frequency,
                Duration = prescriptionMedicationDTO.Duration
            };
            await _context.PrescriptionMedication.AddAsync(prescriptionMedication);
            await _context.SaveChangesAsync();
            prescriptionMedicationDTO.Id = prescriptionMedication.Id;
        }
        public async Task UpdateAsync(PrescriptionMedicationDTO prescriptionMedicationDTO)
        {
            var prescriptionMedication = await _context.PrescriptionMedication
                .FindAsync(prescriptionMedicationDTO.Id);
            if (prescriptionMedication == null) throw new ApplicationException("medicamento no encontrado");
            prescriptionMedication.PrescriptionId = prescriptionMedicationDTO.PrescriptionId;
            prescriptionMedication.MedicationId = prescriptionMedicationDTO.MedicationId;
            prescriptionMedication.Dosage = prescriptionMedicationDTO.Dosage;
            prescriptionMedication.Frequency = prescriptionMedicationDTO.Frequency;
            prescriptionMedication.Duration = prescriptionMedicationDTO.Duration;
            _context.PrescriptionMedication.Update(prescriptionMedication);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var prescriptionMedication = await _context.PrescriptionMedication
                .FindAsync(id);
            if (prescriptionMedication == null) throw new ApplicationException("Medicamento no encontrado");
            _context.PrescriptionMedication.Remove(prescriptionMedication);
            await _context.SaveChangesAsync();
        }
    }
}
