using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using CoreHealth.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreHealth.Services.Implements
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;
        private ServiceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ServiceDTO>> GetAllAsync()
        {
            var service = await _context.Service
            .Select(static s => new ServiceDTO
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Cost = s.Cost
            })
            .ToListAsync();

            return service;
        }
        public async Task<ServiceDTO> GetByIdAsync(int id)
        {
            var service = await _context.Service
                .DefaultIfEmpty()
                .Select(s => new ServiceDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Cost = s.Cost
                })
                .FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
                throw new ApplicationException("No hay medicamentos asignados");
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
            if (service == null) throw new ApplicationException("Servicio no encontrado");
            service.Name = serviceDTO.Name;
            service.Description = serviceDTO.Description;
            service.Cost = serviceDTO.Cost;
            _context.Service.Update(service);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            var service = await _context.Service
                .FindAsync(id);
            if (service == null) throw new ApplicationException("Servicio no encontrado");
            _context.Service.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
}
