using CoreHealth.DTOs;
using CoreHealth.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreHealth.Controllers
{
    [Route("api/prescriptions-medication")]
    [ApiController]
    public class PrescriptionMedicationController : ControllerBase
    {
        private readonly IPrescriptionMedicationService _prescriptionMedicationService;
        public PrescriptionMedicationController(IPrescriptionMedicationService prescriptionMedicationService)
        {
            _prescriptionMedicationService = prescriptionMedicationService;
        }
        // GET: api/<clinicController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var prescriptionMedications = await _prescriptionMedicationService.GetAllAsync();
            return Ok(prescriptionMedications);
        }

        // GET api/<clinicController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var prescriptionMedication = await _prescriptionMedicationService.GetByIdAsync(id);

            if (prescriptionMedication == null)
            {
                return NotFound(new { message = "El elemento no existe" });     // Respuesta HTTP 404 Not Found con un mensaje
            }

            return Ok(prescriptionMedication);                                                 // Retorna 200 OK con el producto encontrado.
        }

        // POST api/<clinicController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PrescriptionMedicationDTO prescriptionMedicationDTO)
        {
            try
            {
                await _prescriptionMedicationService.AddAsync(prescriptionMedicationDTO);
                return CreatedAtAction(nameof(GetById), new { id = prescriptionMedicationDTO.Id }, prescriptionMedicationDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hubo un error al agregar elemento: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PrescriptionMedicationDTO prescriptionMedicationDTO)
        {
            if (id != prescriptionMedicationDTO.Id)
                return BadRequest(new { message = "El ID proporcionado no coincide con el objeto" });

            try
            {
                await _prescriptionMedicationService.UpdateAsync(prescriptionMedicationDTO);
                return NoContent(); // 204: Indica éxito sin devolver contenido adicional
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al actualizar el medicamento en la receta: {ex.Message}" });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _prescriptionMedicationService.DeleteAsync(id);
                return NoContent(); // 204: Confirma la eliminación sin devolver datos
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message }); // Si no existe
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al eliminar un medicamento: {ex.Message}" });
            }
        }
    }
}
