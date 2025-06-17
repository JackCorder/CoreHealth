using CoreHealth.Constants;
using CoreHealth.DTOs;
using CoreHealth.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreHealth.Controllers
{
    [Route("api/v1/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _serviceService.GetAllAsync();
            return Ok(services);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _serviceService.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound(new { message = Messages.Error.ServiceNotFound });     // Respuesta HTTP 404 Not Found con un mensaje
            }

            return Ok(service);                                                 // Retorna 200 OK con el producto encontrado.
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ServiceDTO serviceDTO)
        {
            try
            {
                await _serviceService.AddAsync(serviceDTO);
                return CreatedAtAction(nameof(GetById), new { id = serviceDTO.Id }, serviceDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = Messages.Error.ServiceCreateError + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ServiceDTO serviceDTO)
        {
            if (id != serviceDTO.Id)
                return BadRequest(new { message = "El ID proporcionado no coincide con el objeto" });

            try
            {
                await _serviceService.UpdateAsync(serviceDTO);
                return NoContent(); 
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = Messages.Error.ServiceUpdateError + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceService.DeleteAsync(id);
                return NoContent(); // 204: Confirma la eliminación sin devolver datos
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message }); // Si no existe
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = Messages.Error.ServiceDeleteError + ex.Message });
            }
        }

    }
}
