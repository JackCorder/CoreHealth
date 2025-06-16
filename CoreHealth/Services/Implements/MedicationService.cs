using CoreHealth.Constants;
using CoreHealth.DTOs;
using CoreHealth.Models;
using CoreHealth.Services.Interfaces;
using CoreHealth.Settings;
using EcommerceRESTGen6.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoreHealth.Services.Implements
{
    public class MedicationService: IMedicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UploadSettings _uploadSettings; //Configuración de carga de archivos
        private readonly IWebHostEnvironment _env; //Entorno web para acceder a la ruta del servidor

        public MedicationService(ApplicationDbContext context, IOptions<UploadSettings> uploadSettings, IWebHostEnvironment env) { 
            _context = context;
            _uploadSettings = uploadSettings.Value; //Obtiene la configuración de carga de archivos desde las opciones inyectadas
            _env = env;
        }

        //-------------------------------------------------------------------------------------------------\\
        public async Task<List<MedicationDTO>> GetAllAsync()
        {
            var medications = await _context.Medication
                .Select(m => new MedicationDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Presentation = m.Presentation,
                    AdministrationWay = m.AdministrationWay,
                    UrlImage = m.urlImage
                })
                .ToListAsync();

            return medications;
        }


        public async Task<MedicationDTO> GetByIdAsync(int id)
        {
            var medication = await _context.Medication
                 .Select(m => new MedicationDTO //Proyección a BrandDTO para devolver solo los datos necesarios
                 {
                     Id = m.Id,
                     Name = m.Name,
                     Description = m.Description,
                     Presentation = m.Presentation,
                     AdministrationWay = m.AdministrationWay,
                     UrlImage = m.urlImage
                 })
                 .FirstOrDefaultAsync(m => m.Id == id);

            //No validamos si es null, ya que la valdación se realiza en el controlador
            return medication;
        }


        public async Task AddAsync(MedicationDTO medicationDTO)
        {
            //Cargar la imagen en el servidor y obtener la URL
            var urlImagen = await UploadImage(medicationDTO.File); //Llama al método UploadImage para cargar la imagen y obtener la URL

            var medication = new Medication
            {
                Name = medicationDTO.Name,
                Description = medicationDTO.Description,
                Presentation = medicationDTO.Presentation,
                AdministrationWay = medicationDTO.AdministrationWay,
                urlImage = urlImagen
            };

            await _context.Medication.AddAsync(medication);
            await _context.SaveChangesAsync();

            medicationDTO.Id = medication.Id; ///Asigna el ID de la marca creada al DTO para su uso posterior   
        }



        public async Task UpdateAsync(MedicationDTO medicationDTO)
        {
            var medication = await _context.Medication.FindAsync(medicationDTO.Id);

            if (medication == null)
            {
                throw new ApplicationException(Messages.Error.MedicationNotFound); // Si no se encuentra, lanza una excepción
            }

            //Cargar la imagen en el servidor y obtener la URL
            var urlImage = await UploadImage(medicationDTO.File);

            medication.Name = medicationDTO.Name;
            medication.Description = medicationDTO.Description;
            medication.Presentation = medicationDTO.Presentation;
            medication.AdministrationWay = medicationDTO.AdministrationWay;
            //Si no hay una imagen cargada, se mantiene la URL existente
            medication.urlImage = string.IsNullOrEmpty(medicationDTO.File?.FileName) ? medication.urlImage : urlImage; // Asigna la URL de la imagen cargada o mantiene la existente si no hay nueva imagen

            //Guardar los cambios en la base de datos
            _context.Medication.Update(medication);
            _context.SaveChanges();


        }


        public async Task DeleteAsync(int id)
        {
            var medication = await _context.Medication.FindAsync(id);
            if (medication == null)
            {
                throw new ApplicationException(Messages.Error.MedicationNotFound); 
            }

            _context.Remove(medication);
            _context.SaveChanges();

        }

        //----------------------- Imágenes ----------------------------------\\
        // ------Método para cargar la imagen ---------------------------------------------------------------------------------------------------
        private async Task<string> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return string.Empty; // Si no hay archivo, retornar cadena vacía
            }

            ValidateFile(file);

            string _customPath = Path.Combine(Directory.GetCurrentDirectory(), _uploadSettings.UploadDirectory); //Define la ruta personalizada para guardar las imágenes
            //string _customPath = Path.Combine(_env.WebRootPath, _uploadSettings.UploadDirectory); //Automaticamente creaba un directorio en wwwroot/uploads

            if (!Directory.Exists(_customPath))   // Crear el directorio si no existe
            {
                Directory.CreateDirectory(_customPath);
            }

            // Generar el nombre único del archivo
            var fileName = Path.GetFileNameWithoutExtension(file.FileName)
                            + Guid.NewGuid().ToString()
                            + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_customPath, fileName);

            // Guardar el archivo
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Retornar la ruta relativa o completa, según sea necesario
            return $"/{_uploadSettings.UploadDirectory}/{fileName}";
        }

        //Validar el archivo de imagen 
        private void ValidateFile(IFormFile file)
        {
            if (file == null)
            {
                return; // No file provided, skip validation
            }

            var permittedExtensions = _uploadSettings.AllowedExtensions.Split(',');
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!permittedExtensions.Contains(extension))
            {
                throw new NotSupportedException(Messages.Validation.UnSupportedFileType);
            }
        }
    }
}
