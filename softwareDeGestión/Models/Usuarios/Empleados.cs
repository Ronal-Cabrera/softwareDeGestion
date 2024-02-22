using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Usuarios
{
    public class Empleados
    {
        public Empleados()
        {
        }

        public int? EmpledoID {  get; set; } 

        [Required(ErrorMessage = "Nombres es requerido")]
        [StringLength(30, ErrorMessage = "Logitud máxima 30")]
        public string? nombres_empleado { get; set; }

        [Required(ErrorMessage = "Apellidos es requerido")]
        [StringLength(30, ErrorMessage = "Logitud máxima 30")]
        public string? apellidos_empleado { get; set; }


        [Required(ErrorMessage = "Teléfono es requerido")]
        [StringLength(8, ErrorMessage = "Logitud máxima 8")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido")]
        public string? telefono_empleado { get; set; }

        [Required(ErrorMessage = "Dirección es requerido")]
        [StringLength(10, ErrorMessage = "Logitud máxima 10")]
        public string? direccion_empleado { get; set; }


        public string? fecha_creacion_empleado { get; set; }
    }
}
