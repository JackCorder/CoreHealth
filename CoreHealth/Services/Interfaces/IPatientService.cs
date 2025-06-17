using CoreHealth.DTOs;

namespace CoreHealth.Services.Interfaces
{
    public interface IPatientService
    {
        /// <summary>
        /// Obtener todos los Pacientes
        /// </summary>
        /// <returns></returns>
        Task<List<PatientDTO>> GetAllAsync();

        /// <summary>
        /// Obtener un paciente por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PatientDTO> GetByIdAsync(int id);

        /// <summary>
        /// Crear un registro de un nuevo paciente
        /// </summary>
        /// <param name="PatientDTO"></param>
        /// <returns></returns>
        Task AddAsync(PatientDTO patientDTO);


        /// <summary>
        /// Editar un paciente existente
        /// </summary>
        /// <param name="PatientDTO"></param>
        /// <returns></returns>
        Task UpdateAsync(PatientDTO patientDTO);


        /// <summary>
        /// Eliminar un medicamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
