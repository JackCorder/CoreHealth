using CoreHealth.DTOs;
using CoreHealth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreHealth.Controllers
{
    [Route("api/v1/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        // GET: api/<clinicController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clinics = await _doctorService.GetAllAsync();
            return Ok(clinics);
        }

        // GET api/<clinicController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var clinic = await _doctorService.GetByIdAsync(id);

            if (clinic == null)
            {
                return NotFound(new { message = "La marca no existe" });     // Respuesta HTTP 404 Not Found con un mensaje
            }

            return Ok(clinic);                                                 // Retorna 200 OK con el producto encontrado.
        }

        // POST api/<clinicController>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] DoctorDTO doctorDTO)
        {
            try
            {
                await _doctorService.AddAsync(doctorDTO);
                return CreatedAtAction(nameof(GetById), new { id = doctorDTO.Id }, doctorDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hubo un error al crear la marca: {ex.Message}" });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] DoctorDTO doctorDTO)
        {
            if (id != doctorDTO.Id)
                return BadRequest(new { message = "El ID proporcionado no coincide con el objeto" });

            try
            {
                await _doctorService.UpdateAsync(doctorDTO);
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
                await _doctorService.DeleteAsync(id);
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
