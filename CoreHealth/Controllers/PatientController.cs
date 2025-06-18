using CoreHealth.DTOs;
using CoreHealth.Services.Implements;
using CoreHealth.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreHealth.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patient = await _patientService.GetAllAsync();
            return Ok(patient);
        }

        // GET by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);

            if (patient == null)
            {
                return NotFound(new { message = "El paciente no existe en los registros" });// Respuesta HTTP 404 Not Found con un mensaje
            }

            return Ok(patient);    // Retorna 200 OK con el producto encontrado.
        }

        // POST 
        /// <summary>
        /// Crea un nuevo Paciente.
        /// </summary>
        /// <param name="patientDTO">Datos del Paciente a crear.</param>
        /// <returns>Paciente creado.</returns>
        /// <response code="201">Devuelve el nuevo Paciente creado.</response>
        /// <response code="400">Si los datos proporcionados no son válidos.</response>
        [HttpPost]
        public async Task<ActionResult<PatientDTO>> Create([FromBody] PatientDTO patientDTO)
        {

            try
            {
                await _patientService.AddAsync(patientDTO);

                return CreatedAtAction(nameof(GetById), new { id = patientDTO.Id }, patientDTO);    // Retorna 201 Created con la información del nuevo producto.
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hubo un error al crear el paciente: {ex.Message}" });   // Retorna 400 Bad Request con un mensaje de error.
            }
        }

        /// <summary>
        /// Actualiza un paciente existente.
        /// </summary>
        /// <param name="id">ID del paciente a actualizar.</param>
        /// <param name="patientDTO">Datos actualizados del Paciente.</param>
        /// <returns>NoContent si la actualización es exitosa.</returns>
        /// <response code="204">Si la actualización es exitosa.</response>
        /// <response code="400">Si los datos proporcionados no son válidos o los ID no coinciden.</response>
        /// <response code="404">Si el Paciente no se encuentra.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PatientDTO patientDTO)
        {
            if (id != patientDTO.Id)
            {
                return BadRequest(new { message = "El ID de la ruta no coincide con el ID del medicamento" }); // Respuesta HTTP 400 Bad Request con un mensaje.
            }


            var existingProduct = await _patientService.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound(new { message = "Medicamento no encontrado" });    // Respuesta HTTP 404 Not Found con un mensaje.
            }

            try
            {
                await _patientService.UpdateAsync(patientDTO);

                return NoContent();                                             // Retorna 204 No Content para indicar que la operación fue exitosa.
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Hubo un error al actualizar la información del paciente: {ex.Message}" });  // Retorna 400 Bad Request con un mensaje de error.
            }
        }

        /// <summary>
        /// Elimina un Paciente por su ID.
        /// </summary>
        /// <param name="id">ID del paciente a eliminar.</param>
        /// <returns>NoContent si la eliminación es exitosa.</returns>
        /// <response code="204">Si la eliminación es exitosa.</response>
        /// <response code="404">Si el Paciente no se encuentra.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingPatient = await _patientService.GetByIdAsync(id);
                if (existingPatient == null)
                {
                    return NotFound(new { message = "Producto no encontrado" });    // Respuesta HTTP 404 Not Found con un mensaje.
                }

                await _patientService.DeleteAsync(id);

                return NoContent();                                                 // Retorna 204 No Content para indicar que la operación fue exitosa.
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        message = $"Error al eliminar el paciente: {ex.Message}"
                    });   // Retorna 500 Internal Server Error con un mensaje de error.
            }
        }
    }
}
