namespace CoreHealth.Constants
{
    public class Messages
    {
        public static class Success
        {
            // Creación
            public const string MedicationCreated = "Medicamento agregado correctamente";
            public const string PatientCreated = "Paciente agregado correctamente";
            public const string DoctorCreated = "Médico agregado correctamente";
            public const string OfficeCreated = "Consultorio médico agregado correctamente";
            public const string ServiceCreated = "Servicio médico agregado correctamente";
            public const string AppointmentCreated = "Cita creada correctamente";
            public const string PrescriptionCreated = "Receta médica creada correctamente";
            public const string PrescriptionMedicationCreated = "Medicamento agregado a la receta correctamente";
            public const string MedicalRecordCreated = "Historial médico creado correctamente";

            // Actualización
            public const string MedicationUpdated = "Medicamento actualizado correctamente";
            public const string PatientUpdated = "Paciente actualizado correctamente";
            public const string DoctorUpdated = "Médico actualizado correctamente";
            public const string OfficeUpdated = "Consultorio médico actualizado correctamente";
            public const string ServiceUpdated = "Servicio médico actualizado correctamente";
            public const string AppointmentUpdated = "Cita actualizada correctamente";
            public const string PrescriptionUpdated = "Receta médica actualizada correctamente";
            public const string MedicalRecordUpdated = "Historial médico actualizado correctamente";

            // Eliminación
            public const string MedicationDeleted = "Medicamento eliminado correctamente";
            public const string PatientDeleted = "Paciente eliminado correctamente";
            public const string DoctorDeleted = "Médico eliminado correctamente";
            public const string OfficeDeleted = "Consultorio médico eliminado correctamente";
            public const string ServiceDeleted = "Servicio médico eliminado correctamente";
            public const string AppointmentDeleted = "Cita eliminada correctamente";
            public const string PrescriptionDeleted = "Receta médica eliminada correctamente";
            public const string MedicalRecordDeleted = "Historial médico eliminado correctamente";
        }

        public static class Error
        {
            // Búsqueda/Existencia
            public const string MedicationNotFoundWithId = "No se encontró el medicamento con ID {0}";
            public const string MedicationNotFound = "Medicamento no encontrado";
            public const string PatientNotFound = "Paciente no encontrado";
            public const string DoctorNotFound = "Médico no encontrado";
            public const string OfficeNotFound = "Consultorio médico no encontrado";
            public const string ServiceNotFound = "Servicio médico no encontrado";
            public const string AppointmentNotFound = "Cita no encontrada";
            public const string PrescriptionNotFound = "Receta médica no encontrada";
            public const string MedicalRecordNotFound = "Historial médico no encontrado";

            // Creación
            public const string MedicationCreateError = "Error al agregar medicamento";
            public const string PatientCreateError = "Error al agregar paciente";
            public const string DoctorCreateError = "Error al agregar médico";
            public const string OfficeCreateError = "Error al agregar consultorio médico";
            public const string ServiceCreateError = "Error al agregar servicio médico";
            public const string AppointmentCreateError = "Error al crear cita";
            public const string PrescriptionCreateError = "Error al crear receta médica";
            public const string MedicalRecordCreateError = "Error al crear historial médico";

            // Actualización
            public const string MedicationUpdateError = "Error al actualizar medicamento";
            public const string PatientUpdateError = "Error al actualizar paciente";
            public const string DoctorUpdateError = "Error al actualizar médico";
            public const string OfficeUpdateError = "Error al actualizar consultorio médico";
            public const string ServiceUpdateError = "Error al actualizar servicio médico";
            public const string AppointmentUpdateError = "Error al actualizar cita";
            public const string PrescriptionUpdateError = "Error al actualizar receta médica";
            public const string MedicalRecordUpdateError = "Error al actualizar historial médico";

            // Eliminación
            public const string MedicationDeleteError = "Error al eliminar medicamento";
            public const string PatientDeleteError = "Error al eliminar paciente";
            public const string DoctorDeleteError = "Error al eliminar médico";
            public const string OfficeDeleteError = "Error al eliminar consultorio médico";
            public const string ServiceDeleteError = "Error al eliminar servicio médico";
            public const string AppointmentDeleteError = "Error al eliminar cita";
            public const string PrescriptionDeleteError = "Error al eliminar receta médica";
            public const string MedicalRecordDeleteError = "Error al eliminar historial médico";

            // Restricciones
            public const string MedicationCannotBeDeleted = "No se puede eliminar el medicamento porque está referenciado en recetas";
            public const string DoctorCannotBeDeleted = "No se puede eliminar al médico porque tiene citas asociadas";
            public const string PatientCannotBeDeleted = "No se puede eliminar al paciente porque tiene historiales médicos";
        }

        public static class Validation
        {
            public const string UnsupportedFileType = "Tipo de archivo no soportado";
            public const string InvalidMedicalLicense = "Número de licencia médica inválido";
            public const string InvalidPatientAge = "Edad del paciente inválida";
            public const string AppointmentTimeConflict = "Existe un conflicto de horario con otra cita";
        }

        public static class Info
        {
            public const string NoUpcomingAppointments = "No se encontraron citas próximas";
            public const string NoActivePrescriptions = "No hay recetas activas para este paciente";
        }
    }
}