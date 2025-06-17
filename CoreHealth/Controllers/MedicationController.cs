using CoreHealth.DTOs;
using CoreHealth.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreHealth.Controllers
{
    [Route("api/medications")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationService _medicationService;

        public MedicationController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var medications = await _medicationService.GetAllAsync();
            return Ok(medications);
        }

        // GET by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var medication = await _medicationService.GetByIdAsync(id);

            if (medication == null)
            {
                return NotFound(new { message = "El medicamento no existe" });// Respuesta HTTP 404 Not Found con un mensaje
            }

            return Ok(medication);    // Retorna 200 OK con el producto encontrado.
        }

        // POST 
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] MedicationDTO medicationDTO)
        {
            try
            {
                await _medicationService.AddAsync(medicationDTO);
                return CreatedAtAction(nameof(GetById), new { id = medicationDTO.Id }, medicationDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hubo un error al crear el medicamento: {ex.Message}" });
            }
        }

        /// <summary>
        /// Actualiza un medicamento existente.
        /// </summary>
        /// <param name="id">ID del medicamento a actualizar.</param>
        /// <param name="medicationDTO">Datos actualizados del medicamento.</param>
        /// <returns>NoContent si la actualización es exitosa.</returns>
        /// <response code="204">Si la actualización es exitosa.</response>
        /// <response code="400">Si los datos proporcionados no son válidos o los ID no coinciden.</response>
        /// <response code="404">Si el medicamento no se encuentra.</response>
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] MedicationDTO medicationDTO)         {
            if (id != medicationDTO.Id)
            {
                return BadRequest(new { message = "El ID de la ruta no coincide con el ID del medicamento" });
            }

            try
            {
                await _medicationService.UpdateAsync(medicationDTO);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hubo un error al actualizar el medicamento: {ex.Message}" });
            }
        }

        /// <summary>
        /// Elimina un medicamento por su ID.
        /// </summary>
        /// <param name="id">ID del medicamento a eliminar.</param>
        /// <returns>NoContent si la eliminación es exitosa.</returns>
        /// <response code="204">Si la eliminación es exitosa.</response>
        /// <response code="404">Si el medicamento no se encuentra.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingProduct = await _medicationService.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return NotFound(new { message = "Medicamento no encontrado" });    // Respuesta HTTP 404 Not Found con un mensaje.
                }

                await _medicationService.DeleteAsync(id);

                return NoContent();                                                 // Retorna 204 No Content para indicar que la operación fue exitosa.
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        message = $"Error al eliminar el medicamento: {ex.Message}"
                    });   // Retorna 500 Internal Server Error con un mensaje de error.
            }
        }
    }
}
