using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using CoreHealth.Settings;
using CoreHealth.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CoreHealth.Constants;

namespace CoreHealth.Services.Implements
{
    public class ClinicHistoryService : IClinicHistoryService 
    {
        private readonly ApplicationDbContext _context;
        public ClinicHistoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ClinicHistoryDTO>> GetAllAsync()
        {
            var clinicHistorys = await _context.ClinicHistory
            .Where(ch => !ch.IsDelete)
            .Select(static ch => new ClinicHistoryDTO
            {
                Id = ch.Id,
                Date = ch.Date,
                PatientId = ch.PatientId,
                PatientName = ch.Patient.Name,
                Description = ch.Description,
                Active = ch.Active
            })
            .ToListAsync();

            return clinicHistorys;
        }
        public async Task<ClinicHistoryDTO> GetByIdAsync(int id)
        {
            var clinicHistory = await _context.ClinicHistory
                .DefaultIfEmpty()
                .Select(ch => new ClinicHistoryDTO
                {
                    Id = ch.Id,
                    Date = ch.Date,
                    PatientId = ch.PatientId,
                    PatientName = ch.Patient.Name,
                    Description = ch.Description,
                    Active = ch.Active
                })
                .FirstOrDefaultAsync(ch => ch.Id == id);
            if (clinicHistory == null)
                throw new ApplicationException("No hay historial clinico");
            return clinicHistory;
        }

        public async Task AddAsync(ClinicHistoryDTO clinicHistoryDTO)
        {
            var clinicHistory = new ClinicHistory
            {
                Date = clinicHistoryDTO.Date,
                PatientId = clinicHistoryDTO.PatientId,
                Description = clinicHistoryDTO.Description
            };
            await _context.ClinicHistory.AddAsync(clinicHistory);
            await _context.SaveChangesAsync();
            clinicHistoryDTO.Id = clinicHistory.Id;
        }
        public async Task UpdateAsync(ClinicHistoryDTO clinicHistoryDTO)
        {
            var clinicHistory = await _context.ClinicHistory
                .FindAsync(clinicHistoryDTO.Id);
            if (clinicHistory == null) throw new ApplicationException("historial clinico no encontrado");
            clinicHistory.Date = clinicHistoryDTO.Date;
            clinicHistory.PatientId = clinicHistoryDTO.PatientId;
            clinicHistory.Description = clinicHistoryDTO.Description;
            clinicHistory.Active = clinicHistoryDTO.Active;
            _context.ClinicHistory.Update(clinicHistory);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var clinicHistory = await _context.ClinicHistory
                .FindAsync(id);
            if (clinicHistory == null) throw new ApplicationException(Messages.Error.MedicalRecordNotFound);

            if (clinicHistory.IsDelete) throw new ApplicationException(Messages.Error.MedicalRecordDeleteError);
            clinicHistory.IsDelete = true;
            _context.ClinicHistory.Update(clinicHistory);
            await _context.SaveChangesAsync();
        }
    }
}
