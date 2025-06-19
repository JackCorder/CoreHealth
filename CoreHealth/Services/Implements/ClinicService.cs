using CoreHealth.Constants;
using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using CoreHealth.Settings;
using CoreHealth.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreHealth.Services.Implements
{
    public class ClinicService : IClinicService
    {
        private readonly ApplicationDbContext _context;
        public ClinicService(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<List<ClinicDTO>> GetAllAsync() {
            var clinics = await _context.Clinic
                .SelectMany(c => _context.Doctor
                .Where(d=> d.Id == c.DoctorId && !c.IsDelete)
                .DefaultIfEmpty(),
                (c,d)=> new ClinicDTO { 
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    DoctorId = c.DoctorId,
                    DoctorName = d.Name != null ? d.Name:"Doctor no asignado a este consultorio",
                    Active = c.Active,

                }
                )
                .ToListAsync();
            return clinics;
        }
        public async Task<ClinicDTO> GetByIdAsync(int id)
        {
            var clinic = await _context.Clinic
                .DefaultIfEmpty()
                .Select(c => new ClinicDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    DoctorId = c.DoctorId != null ? c.DoctorId:0,
                    Active = c.Active,
                    IsDelete = c.IsDelete,
                    HighSystem = c.HighSystem

                })
                .FirstOrDefaultAsync(c => c.Id == id);
            if (clinic == null)
                throw new ApplicationException("Consultorio no encontrado");
            return clinic;
        }
        public async Task AddAsync(ClinicDTO clinicDTO)
        {
            var clinic = new Clinic
            {
                Name = clinicDTO.Name,
                Description = clinicDTO.Description,
                DoctorId = clinicDTO.DoctorId,
                Active = clinicDTO.Active,
                IsDelete= clinicDTO.IsDelete,
                HighSystem= clinicDTO.HighSystem
            };
            await _context.Clinic.AddAsync(clinic);
            await _context.SaveChangesAsync();
            clinicDTO.Id = clinic.Id;
        }
        public async Task UpdateAsync(ClinicDTO clinictDTO)
        {
            var clinic = await _context.Clinic
                .FindAsync(clinictDTO.Id);
            if (clinic == null) throw new ApplicationException("Consultorio no encontrado");
            clinic.Name = clinictDTO.Name;
            clinic.Description = clinictDTO.Description;           
            clinic.DoctorId = clinictDTO.DoctorId;
            clinic.Active = clinictDTO.Active;
            clinic.IsDelete = clinictDTO.IsDelete;
            clinic.HighSystem = clinictDTO.HighSystem;
            _context.Clinic.Update(clinic);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var clinic = await _context.Clinic
                .FindAsync(id);
            if (clinic == null) throw new ApplicationException("Consultorio no encontrado");
            clinic.IsDelete = true;
            clinic.Active = false;
            await _context.SaveChangesAsync();
        }
    }
}