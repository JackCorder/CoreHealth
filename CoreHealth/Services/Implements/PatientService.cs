using CoreHealth.Constants;
using CoreHealth.Data;
using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CoreHealth.Services.Implements
{
    public class PatientService: IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        //-------------------------------------------------------------------------------------------------\\
        public async Task<List<PatientDTO>> GetAllAsync()
        {
            var patients = await _context.Patient
                .Where(p => !p.IsDelete)
                .Select(p => new PatientDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Gender = p.Gender,
                    BirthDate = p.BirthDate,
                    Address = p.Address,
                    Phone = p.Phone,
                    Email = p.Email,
                    NSS = p.NSS
                })
                .ToListAsync();

            return patients;
        }

        public async Task<PatientDTO> GetByIdAsync(int id)
        {
            var patient = await _context.Patient
                .Select(p => new PatientDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Gender = p.Gender,
                    BirthDate = p.BirthDate,
                    Address = p.Address,
                    Phone = p.Phone,
                    Email = p.Email,
                    NSS = p.NSS
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                throw new ApplicationException(Messages.Error.PatientNotFound);
            }

            return patient;
        }
        public async Task AddAsync(PatientDTO patientDTO)
        {
            // Verificar si ya existe el NSS
            bool exists = await _context.Patient.AnyAsync(p => p.NSS == patientDTO.NSS);

            if (exists)
            {
                throw new ApplicationException(Messages.Error.PatientNSSExist);
            }

            var patient = new Patient
            {
                Name = patientDTO.Name,
                Gender = patientDTO.Gender,
                BirthDate = patientDTO.BirthDate,
                Address = patientDTO.Address,
                Phone = patientDTO.Phone,
                Email = patientDTO.Email,
                NSS = patientDTO.NSS
            };

            await _context.Patient.AddAsync(patient);
            await _context.SaveChangesAsync();

            patientDTO.Id = patient.Id;
        }
        public async Task UpdateAsync(PatientDTO patientDTO)
        {
            var patient = await _context.Patient.FindAsync(patientDTO.Id);

            if (patient == null)
            {
                throw new ApplicationException(Messages.Error.PatientNotFound);
            }

            patient.Name = patientDTO.Name;
            patient.Gender = patientDTO.Gender;
            patient.BirthDate = patientDTO.BirthDate;
            patient.Address = patientDTO.Address;
            patient.Phone = patientDTO.Phone;
            patient.Email = patientDTO.Email;
            patient.NSS = patientDTO.NSS;

            _context.Patient.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patient.FindAsync(id);

            if (patient == null)
            {
                throw new ApplicationException(Messages.Error.PatientNotFound);
            }

            patient.IsDelete = true;
            _context.Update(patient);
            await _context.SaveChangesAsync();
        }
    }
}
