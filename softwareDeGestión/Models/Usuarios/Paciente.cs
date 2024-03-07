using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Usuarios
{
    public class Paciente
    {
        public Paciente()
        {
        }

        public int? PacienteID { get; set; }

        [Required(ErrorMessage = "Nombres es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Apellidos es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? Apellido { get; set; }


        [Required(ErrorMessage = "Edad es requerido")]
        [Range(1, 150, ErrorMessage = "Rago de Edad, 1 a 150")]
        public int? Edad { get; set; }

        [Required(ErrorMessage = "Genero es requerido")]
        [StringLength(10, ErrorMessage = "Logitud máxima 10")]
        public string? Genero { get; set; }

        [Required(ErrorMessage = "Dirección es requerido")]
        [StringLength(100, ErrorMessage = "Logitud máxima 100")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "Telefono es requerido")]
        [StringLength(8, ErrorMessage = "Requiere 8 números")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Ingrese solo números")]
        public string? Telefono { get; set; }

        public string? FechaIngreso { get; set; }
    }
}
