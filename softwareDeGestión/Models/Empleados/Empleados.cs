using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Empleados
{
    public class Empleados
    {
        public Empleados()
        {
        }

        public int? EmpledoID { get; set; }

        [Required(ErrorMessage = "Nombres es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? nombres_empleado { get; set; }

        [Required(ErrorMessage = "Apellidos es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? apellidos_empleado { get; set; }


        [Required(ErrorMessage = "Telefono es requerido")]
        [StringLength(8, ErrorMessage = "Requiere 8 números")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Ingrese solo números")]
        public string? telefono_empleado { get; set; }

        [Required(ErrorMessage = "Dirección es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 50.")]
        public string? direccion_empleado { get; set; }

        public string? fecha_creacion_empleado { get; set; }
    }
}
