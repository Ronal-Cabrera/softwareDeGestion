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
        public string? name { get; set; }
        public string? FechaRegistro { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 100.")]
        public string? TipoActividad { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? DuracionActividad { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? IntensidadActividad { get; set; }
    }
}
