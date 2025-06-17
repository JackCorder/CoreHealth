using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using CoreHealth.Settings;
using CoreHealth.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreHealth.Services.Implements
{
    public class DoctorService:IDoctorService
    {
        private readonly ApplicationDbContext _context;
        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<DoctorDTO>> GetAllAsync()
        {
            var doctors = await _context.Doctor
                .Select(static d => new DoctorDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Area = d.Area,
                    License = d.License,
                    Phone = d.Phone,
                    Email = d.Email,
                    Active = d.Active,
                    IsDelete = d.IsDelete,
                    HighSystem = d.HighSystem,
                })
                .ToListAsync();

            return doctors;
        }
        public async Task<DoctorDTO> GetByIdAsync(int id)
        {
            var doctor = await _context.Doctor
                .Where(d => d.Id == id)
                .Select(d => new DoctorDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                    Area = d.Area,
                    License = d.License,
                    Phone = d.Phone,
                    Email = d.Email,
                    Active= d.Active,
                    IsDelete = d.IsDelete,
                    HighSystem = d.HighSystem,
                })
                .FirstOrDefaultAsync();

            if (doctor == null)
                throw new ApplicationException("Doctor no encontrado");

            return doctor;
        }
        public async Task AddAsync(DoctorDTO doctorDTO)
        {
            var doctor = new Doctor
            {
                Name = doctorDTO.Name,
                Area = doctorDTO.Area,
                License = doctorDTO.License,
                Phone = doctorDTO.Phone,
                Email = doctorDTO.Email,
                Active = doctorDTO.Active,
                IsDelete = doctorDTO.IsDelete,
                HighSystem = doctorDTO.HighSystem,
            };

            await _context.Doctor.AddAsync(doctor);
            await _context.SaveChangesAsync();

            doctorDTO.Id = doctor.Id;
        }
        public async Task UpdateAsync(DoctorDTO doctorDTO)
        {
            var doctor = await _context.Doctor.FindAsync(doctorDTO.Id);
            if (doctor == null) throw new ApplicationException("Doctor no encontrado");

            doctor.Name = doctorDTO.Name;
            doctor.Area = doctorDTO.Area;
            doctor.License = doctorDTO.License;
            doctor.Phone = doctorDTO.Phone;
            doctor.Email = doctorDTO.Email;

            _context.Doctor.Update(doctor);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null) throw new ApplicationException("Doctor no encontrado");
            doctor.IsDelete = true;
            doctor.Active = false;
            await _context.SaveChangesAsync();
        }



    }
}
