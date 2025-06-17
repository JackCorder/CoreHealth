using CoreHealth.Constants;
using CoreHealth.Data;
using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreHealth.Services.Implements
{
    public class PrescriptionService: IPrescriptionService
    {

        private readonly ApplicationDbContext _context;

        public PrescriptionService(ApplicationDbContext context)
        {
            _context = context;
        }

        //------------------------------------------------------------------\\
        public async Task<List<PrescriptionDTO>> GetAllAsync()
        {
            var prescriptions = await _context.Prescription
                .Where(p => !p.IsDelete)
                .Select(p => new PrescriptionDTO
                {
                    Id = p.Id,
                    DoctorId = p.DoctorId,
                    AppointmentId = p.AppointmentId,
                    Date = p.Date,
                    doctorName = p.Doctor.Name
                })
                .ToListAsync();

            return prescriptions;
        }

        public async Task<PrescriptionDTO> GetByIdAsync(int id)
        {
            var prescription = await _context.Prescription
                .Select(p => new PrescriptionDTO
                {
                    Id = p.Id,
                    DoctorId = p.DoctorId,
                    AppointmentId = p.AppointmentId,
                    Date = p.Date,
                    doctorName = p.Doctor.Name
                })
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (prescription == null)
            {
                throw new ApplicationException(Messages.Error.PrescriptionNotFound);
            }
            return prescription;
        }
        public async Task AddAsync(PrescriptionDTO prescriptionDTO)
        {
            var prescription = new Prescription
            {
                DoctorId = prescriptionDTO.DoctorId,
                AppointmentId = prescriptionDTO.AppointmentId,
                Date = prescriptionDTO.Date
            };

            await _context.Prescription.AddAsync(prescription);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(PrescriptionDTO prescriptionDTO)
        {
            var prescription = await _context.Prescription.FindAsync(prescriptionDTO.Id);

            if (prescription == null)
            {
                throw new ApplicationException(Messages.Error.PrescriptionNotFound);
            }

            prescription.DoctorId = prescriptionDTO.DoctorId;
            prescription.AppointmentId = prescriptionDTO.AppointmentId;
            prescription.Date = prescriptionDTO.Date;

            _context.Prescription.Update(prescription);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var prescription = await _context.Prescription.FindAsync(id);

            if (prescription == null)
            {
                throw new ApplicationException(Messages.Error.PrescriptionNotFound);
            }

            prescription.IsDelete = true;

            _context.Prescription.Update(prescription);
            await _context.SaveChangesAsync();
        }


    }
}
