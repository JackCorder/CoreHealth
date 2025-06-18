using CoreHealth.DTOs;

namespace CoreHealth.Services.Interfaces
{
    public interface IPrescriptionService
    {
        /// <summary>
        /// Obtener todos los pacientes
        /// </summary>
        /// <returns></returns>
        Task<List<PrescriptionDTO>> GetAllAsync();

        /// <summary>
        /// Obtener un receta por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PrescriptionDTO> GetByIdAsync(int id);

        /// <summary>
        /// Crear un registro de un nuevo receta
        /// </summary>
        /// <param name="PrescriptionDTO"></param>
        /// <returns></returns>
        Task AddAsync(PrescriptionDTO prescriptionDTO);


        /// <summary>
        /// Editar un receta existente
        /// </summary>
        /// <param name="PrescriptionDTO"></param>
        /// <returns></returns>
        Task UpdateAsync(PrescriptionDTO prescriptionDTO);


        /// <summary>
        /// Eliminar un receta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
