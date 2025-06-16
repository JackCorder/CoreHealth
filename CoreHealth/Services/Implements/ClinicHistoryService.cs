using CoreHealth.DTOs;
using CoreHealth.Services.Interfaces;
using CoreHealth.Settings;
using EcommerceRESTGen6.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreHealth.Services.Implements
{
    public class ClinicHistoryService : IClinicHistoryService 
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private ClinicHistoryService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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

        public async Task AddAsync(AppointmentDTO AppointmentDTO)
        {
            var Appointment = new Appointment
            {
                Id = AppointmentDTO.Id,
                Date = AppointmentDTO.Date,
                PatientId = AppointmentDTO.PatientId,
                ClinicId = AppointmentDTO.ClinicId,
                Reason = AppointmentDTO.Reason,
                Diagnostic = AppointmentDTO.Diagnostic,
                Treatment = AppointmentDTO.Treatment,
                ServiceId = AppointmentDTO.ServiceId
            };
            await _context.Appointment.AddAsync(Appointment);
            await _context.SaveChangesAsync();
            AppointmentDTO.Id = Appointment.Id;
        }
        public async Task UpdateAsync(AppointmentDTO AppointmenttDTO)
        {
            var Appointment = await _context.Appointment
                .FindAsync(AppointmenttDTO.Id);
            if (Appointment == null) throw new ApplicationException("Consultorio no encontrado");
            Appointment.Date = AppointmenttDTO.Date;
            Appointment.PatientId = AppointmenttDTO.PatientId;
            Appointment.ClinicId = Appointment.ClinicId;
            Appointment.Reason = AppointmenttDTO.Reason;
            Appointment.Diagnostic = AppointmenttDTO.Diagnostic;
            Appointment.Treatment = Appointment.Treatment;
            Appointment.ServiceId = AppointmenttDTO.ServiceId;
            _context.Appointment.Update(Appointment);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var Appointment = await _context.Appointment
                .FindAsync(id);
            if (Appointment == null) throw new ApplicationException("Consultorio no encontrado");
            _context.Appointment.Remove(Appointment);
            await _context.SaveChangesAsync();
        }
    }
}
