using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using CoreHealth.Settings;
using CoreHealth.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreHealth.Services.Implements
{
    public class AppointmentService:IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<AppointmentDTO>> GetAllAsync()
        {
            var appointments = await _context.Appointment
        .Select(static a => new AppointmentDTO
        {
            Id = a.Id,
            Date = a.Date,
            PatientId = a.PatientId,
            ClinicId = a.ClinicId,
            Reason = a.Reason,
            Diagnostic = a.Diagnostic,
            Treatment = a.Treatment,
            ServiceId = a.ServiceId
        })
        .ToListAsync();

            return appointments;
        }
        public async Task<AppointmentDTO> GetByIdAsync(int id)
        {
            var appointment = await _context.Appointment
                .DefaultIfEmpty()
                .Select(a=> new AppointmentDTO
                {
                    Id = a.Id,
                    Date = a.Date,
                    PatientId = a.PatientId,
                    ClinicId = a.ClinicId,
                    Reason = a.Reason,
                    Diagnostic = a.Diagnostic,
                    Treatment = a.Treatment,
                    ServiceId = a.ServiceId

                })
                .FirstOrDefaultAsync(a => a.Id == id);
            if (appointment == null)
                throw new ApplicationException("Consultorio no encontrado");
            return appointment;
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
