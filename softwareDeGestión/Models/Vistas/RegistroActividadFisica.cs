using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Vistas
{
    public class RegistroActividadFisica
    {
        public RegistroActividadFisica()
        {
        }

        public int? ActividadID { get; set; }
        public int? PacienteID { get; set; }
        public string? FechaRegistro { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        public string? TipoActividad { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        public string? DuracionActividad { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        public string? IntensidadActividad { get; set; }
    }
}
