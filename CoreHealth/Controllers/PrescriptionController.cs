using CoreHealth.DTOs;
using CoreHealth.Services.Implements;
using CoreHealth.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreHealth.Controllers
{
    [Route("api/prescription")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var prescription = await _prescriptionService.GetAllAsync();
            return Ok(prescription);
        }

        // GET by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var prescription = await _prescriptionService.GetByIdAsync(id);

            if (prescription == null)
            {
                return NotFound(new { message = "La receta no existe en los registros" });// Respuesta HTTP 404 Not Found con un mensaje
            }

            return Ok(prescription);    // Retorna 200 OK con el producto encontrado.
        }

        // POST 
        /// <summary>
        /// Crea un nuevo Paciente.
        /// </summary>
        /// <param name= prescriptionDTO">Datos de la receta a crear.</param>
        /// <returns>Receta creado.</returns>
        /// <response code="201">Devuelve la nueva receta creada.</response>
        /// <response code="400">Si los datos proporcionados no son válidos.</response>
        [HttpPost]
        public async Task<ActionResult<PrescriptionDTO>> Create([FromBody] PrescriptionDTO prescriptionDTO)
        {

            try
            {
                await _prescriptionService.AddAsync(prescriptionDTO);

                return CreatedAtAction(nameof(GetById), new { id = prescriptionDTO.Id }, prescriptionDTO);    // Retorna 201 Created con la información del nuevo producto.
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hubo un error al crear la receta: {ex.Message}" });   // Retorna 400 Bad Request con un mensaje de error.
            }
        }

        /// <summary>
        /// Actualiza una receta existente.
        /// </summary>
        /// <param name="id">ID de la receta a actualizar.</param>
        /// <param name=" prescriptionDTO">Datos actualizados de la receta.</param>
        /// <returns>NoContent si la actualización es exitosa.</returns>
        /// <response code="204">Si la actualización es exitosa.</response>
        /// <response code="400">Si los datos proporcionados no son válidos o los ID no coinciden.</response>
        /// <response code="404">Si la receta no se encuentra.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PrescriptionDTO prescriptionDTO)
        {
            if (id != prescriptionDTO.Id)
            {
                return BadRequest(new { message = "El ID de la ruta no coincide con el ID del medicamento" }); // Respuesta HTTP 400 Bad Request con un mensaje.
            }


            var existingProduct = await _prescriptionService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound(new { message = "Medicamento no encontrado" });    // Respuesta HTTP 404 Not Found con un mensaje.
            }

            try
            {
                await _prescriptionService.UpdateAsync(prescriptionDTO);

                return NoContent();                                             // Retorna 204 No Content para indicar que la operación fue exitosa.
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hubo un error al actualizar la información de la receta: {ex.Message}" });  // Retorna 400 Bad Request con un mensaje de error.
            }
        }

        /// <summary>
        /// Elimina una receta por su ID.
        /// </summary>
        /// <param name="id">ID de la rectea a eliminar.</param>
        /// <returns>NoContent si la eliminación es exitosa.</returns>
        /// <response code="204">Si la eliminación es exitosa.</response>
        /// <response code="404">Si la receta no se encuentra.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingPatient = await _prescriptionService.GetByIdAsync(id);
                if (existingPatient == null)
                {
                    return NotFound(new { message = "receta no encontrada" });    // Respuesta HTTP 404 Not Found con un mensaje.
                }

                await _prescriptionService.DeleteAsync(id);

                return NoContent();                                                 // Retorna 204 No Content para indicar que la operación fue exitosa.
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        message = $"Error al eliminar la receta: {ex.Message}"
                    });   // Retorna 500 Internal Server Error con un mensaje de error.
            }
        }
    }
}
