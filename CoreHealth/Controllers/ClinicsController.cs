using CoreHealth.DTOs;
using CoreHealth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreHealth.Controllers
{
    [Route("api/v1/clinic")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicService _clinicService;
        public ClinicsController(IClinicService clinicService) { 
            _clinicService = clinicService;
        }
        // GET: api/<clinicController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clinics = await _clinicService.GetAllAsync();
            return Ok(clinics);
        }

        // GET api/<clinicController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var clinic = await _clinicService.GetByIdAsync(id);

            if (clinic == null)
            {
                return NotFound(new { message = "La marca no existe" });     // Respuesta HTTP 404 Not Found con un mensaje
            }

            return Ok(clinic);                                                 // Retorna 200 OK con el producto encontrado.
        }

        // POST api/<clinicController>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ClinicDTO clinicDTO)
        {
            try
            {
                await _clinicService.AddAsync(clinicDTO);
                return CreatedAtAction(nameof(GetById), new { id = clinicDTO.Id }, clinicDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hubo un error al crear la marca: {ex.Message}" });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ClinicDTO clinicDTO)
        {
            if (id != clinicDTO.Id)
                return BadRequest(new { message = "El ID proporcionado no coincide con el objeto" });

            try
            {
                await _clinicService.UpdateAsync(clinicDTO);
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
                await _clinicService.DeleteAsync(id);
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
