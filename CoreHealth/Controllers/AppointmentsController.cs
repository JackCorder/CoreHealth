using CoreHealth.DTOs;
using CoreHealth.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreHealth.Controllers
{
    [Route("api/v1/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        // GET: api/<clinicController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clinics = await _appointmentService.GetAllAsync();
            return Ok(clinics);
        }

        // GET api/<clinicController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var clinic = await _appointmentService.GetByIdAsync(id);

            if (clinic == null)
            {
                return NotFound(new { message = "La marca no existe" });     // Respuesta HTTP 404 Not Found con un mensaje
            }

            return Ok(clinic);                                                 // Retorna 200 OK con el producto encontrado.
        }

        // POST api/<clinicController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentDTO appointmentDTO)
        {
            try
            {
                await _appointmentService.AddAsync(appointmentDTO);
                return CreatedAtAction(nameof(GetById), new { id = appointmentDTO.Id }, appointmentDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hubo un error al crear la marca: {ex.Message}" });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentDTO appointmentDTO)
        {
            if (id != appointmentDTO.Id)
                return BadRequest(new { message = "El ID proporcionado no coincide con el objeto" });

            try
            {
                await _appointmentService.UpdateAsync(appointmentDTO);
                return NoContent(); // 204: Indica éxito sin devolver contenido adicional
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message }); // Manejo de error si la marca no existe
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al actualizar la marca: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _appointmentService.DeleteAsync(id);
                return NoContent(); // 204: Confirma la eliminación sin devolver datos
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message }); // Si la marca no existe
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al eliminar la marca: {ex.Message}" });
            }
        }
    }
}
