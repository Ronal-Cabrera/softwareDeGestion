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
        //[StringLength(30, ErrorMessage = "Logitud máxima 30")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Apellidos es requerido")]
        //[StringLength(30, ErrorMessage = "Logitud máxima 30")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "Edad es requerido")]
        //[StringLength(8, ErrorMessage = "Logitud máxima 8")]
        //[Phone(ErrorMessage = "El formato del teléfono no es válido")]
        public string? Edad { get; set; }

        [Required(ErrorMessage = "Genero es requerido")]
        //[StringLength(10, ErrorMessage = "Logitud máxima 10")]
        public string? Genero { get; set; }

        [Required(ErrorMessage = "Dirección es requerido")]
        //[StringLength(10, ErrorMessage = "Logitud máxima 10")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "Telefono es requerido")]
        //[StringLength(8, ErrorMessage = "Logitud máxima 8")]
        //[Phone(ErrorMessage = "El formato del teléfono no es válido")]
        public string? Telefono { get; set; }

        public string? FechaIngreso { get; set; }
    }
}
