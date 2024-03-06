using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Vistas
{
    public class RegistroAlimentacion
    {
        public RegistroAlimentacion()
        {
        }

        public int? AlimentacionID { get; set; }
        public int? PacienteID { get; set; }
        public string? FechaRegistro { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        public string? DescripcionComidas { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        public string? ConteoCarbohidratos { get; set; }

        [Required(ErrorMessage = "Dato requerido.")]
        public string? ObservacionesAlimentacion { get; set; }
    }
}
