using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using CoreHealth.Settings;
using CoreHealth.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoreHealth.Services.Implements
{
    public class ClinicHistoryService : IClinicHistoryService 
    {
        private readonly ApplicationDbContext _context;
        private ClinicHistoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ClinicHistoryDTO>> GetAllAsync()
        {
            var clinicHistorys = await _context.ClinicHistory
            .Select(static a => new ClinicHistoryDTO
            {
                Id = a.Id,
                Date = a.Date,
                PatientId = a.PatientId,
                PatientName = a.Patient.Name,
                Description = a.Description
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
                    Description = ch.Description

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
            _context.ClinicHistory.Update(clinicHistory);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var clinicHistory = await _context.ClinicHistory
                .FindAsync(id);
            if (clinicHistory == null) throw new ApplicationException("Historial clinico no encontrado");
            _context.ClinicHistory.Remove(clinicHistory);
            await _context.SaveChangesAsync();
        }
    }
}
