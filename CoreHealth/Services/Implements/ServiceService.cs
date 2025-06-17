using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using CoreHealth.Data;
using Microsoft.EntityFrameworkCore;
using CoreHealth.Constants;

namespace CoreHealth.Services.Implements
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;
        public ServiceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ServiceDTO>> GetAllAsync()
        {
            var service = await _context.Service
            .Where(s => !s.IsDelete)
            .Select(static s => new ServiceDTO
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Cost = s.Cost,
                Active = s.Active
            })
            .ToListAsync();

            return service;
        }
        public async Task<ServiceDTO> GetByIdAsync(int id)
        {
            var service = await _context.Service
                .Where(s => !s.IsDelete)
                .DefaultIfEmpty()
                .Select(s => new ServiceDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Cost = s.Cost,
                    Active = s.Active
                })
                .FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
                throw new ApplicationException(Messages.Error.ServiceNotFound);
            return service;
        }

        public async Task AddAsync(ServiceDTO serviceDTO)
        {
            var service = new Service
            {
                Name = serviceDTO.Name,
                Description = serviceDTO.Description,
                Cost = serviceDTO.Cost
            };
            await _context.Service.AddAsync(service);
            await _context.SaveChangesAsync();
            serviceDTO.Id = service.Id;
        }
        public async Task UpdateAsync(ServiceDTO serviceDTO)
        {
            var service = await _context.Service
                .FindAsync(serviceDTO.Id);
            if (service == null) throw new ApplicationException(Messages.Error.ServiceNotFound);
            service.Name = serviceDTO.Name;
            service.Description = serviceDTO.Description;
            service.Cost = serviceDTO.Cost;
            service.Active = serviceDTO.Active;
            _context.Service.Update(service);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var service = await _context.Service
                .FindAsync(id);
            
            if (service == null) throw new ApplicationException(Messages.Error.ServiceNotFound);
            if (service.IsDelete) throw new ApplicationException(Messages.Error.MedicalRecordDeleteError);
            service.IsDelete = true;

            _context.Service.Update(service);
            await _context.SaveChangesAsync();
        }
    }
}
