using CoreHealth.DTOs;

namespace CoreHealth.Services.Interfaces
{
    public interface IMedicationService
    {
        /// <summary>
        /// Obtener todos los medicamentos
        /// </summary>
        /// <returns></returns>
        Task<List<MedicationDTO>> GetAllAsync();

        /// <summary>
        /// Obtener un medicamento por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MedicationDTO> GetByIdAsync(int id);

        /// <summary>
        /// Crear un registro de un nuevo medicamento
        /// </summary>
        /// <param name="medicationDTO"></param>
        /// <returns></returns>
        Task AddAsync(MedicationDTO medicationDTO);


        /// <summary>
        /// Editar un medicamento existente
        /// </summary>
        /// <param name="medicationDTO"></param>
        /// <returns></returns>
        Task UpdateAsync(MedicationDTO medicationDTO);


        /// <summary>
        /// Eliminar un medicamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
