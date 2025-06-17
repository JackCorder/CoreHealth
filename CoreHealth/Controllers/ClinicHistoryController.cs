using CoreHealth.Constants;
using CoreHealth.DTOs;
using CoreHealth.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreHealth.Controllers
{
    [Route("api/v1/clinic-histories")]
    [ApiController]
    public class ClinicHistoryController : ControllerBase
    {
        private readonly IClinicHistoryService _clinicHistoryService;
        public ClinicHistoryController(IClinicHistoryService clinicHistoryService)
        {
            _clinicHistoryService = clinicHistoryService;
        }
        // GET: api/<clinicController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clinicHistories = await _clinicHistoryService.GetAllAsync();
            return Ok(clinicHistories);
        }

        // GET api/<clinicController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var clinicHistory = await _clinicHistoryService.GetByIdAsync(id);

            if (clinicHistory == null)
            {
                return NotFound(new { message = Messages.Error.MedicalRecordNotFound });     // Respuesta HTTP 404 Not Found con un mensaje
            }

            return Ok(clinicHistory);                                                 // Retorna 200 OK con el producto encontrado.
        }

        // POST api/<clinicController>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ClinicHistoryDTO clinicHistoryDTO)
        {
            try
            {
                await _clinicHistoryService.AddAsync(clinicHistoryDTO);
                return CreatedAtAction(nameof(GetById), new { id = clinicHistoryDTO.Id }, clinicHistoryDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = Messages.Error.MedicalRecordCreateError + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ClinicHistoryDTO clinicHistoryDTO)
        {
            if (id != clinicHistoryDTO.Id)
                return BadRequest(new { message = Messages.Error.MedicalRecordNotFound});

            try
            {
                await _clinicHistoryService.UpdateAsync(clinicHistoryDTO);
                return NoContent(); // 204: Indica éxito sin devolver contenido adicional
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = Messages.Error.MedicalRecordUpdateError }); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = Messages.Error.MedicalRecordUpdateError + ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _clinicHistoryService.DeleteAsync(id);
                return NoContent(); // 204: Confirma la eliminación sin devolver datos
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message }); // Si no existe
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = Messages.Error.MedicalRecordDeleteError + ex.Message });
            }
        }

    }
}
