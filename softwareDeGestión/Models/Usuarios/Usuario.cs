using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Usuarios
{
    public class Usuario
    {
        public Usuario()
        {
        }

        [Required(ErrorMessage = "Empleado es requerido")]
        public int? EmpleadoID { get; set; }

        [Required(ErrorMessage = "Usuario es requerido")]
        [StringLength(30, ErrorMessage = "Logitud máxima 30")]
        public string? nombre_usuario { get; set; }


        [Required(ErrorMessage = "Contraseña es requerido")]
        [StringLength(30, ErrorMessage = "Logitud máxima 30")]
        public string? password_usuario { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        [StringLength(10, ErrorMessage = "Logitud máxima 10")]
        public string? estado_usuario { get; set; }


        [Required(ErrorMessage = "Rol es requerido")]
        [StringLength(20, ErrorMessage = "Logitud máxima 20")]
        public string? rol_usuario { get; set; }

        public string? fecha_creacion_usuario { get; set; }
    }
}
