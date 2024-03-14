using System.ComponentModel.DataAnnotations;

namespace softwareDeGestión.Models.Usuarios
{
    public class UsuariosPacientes
    {
        public UsuariosPacientes() { }

        public int? codigo_usuario {  get; set; }
        public int? PacienteID { get; set; }


        [Required(ErrorMessage = "Usuario es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 30.")]
        public string? nombre_usuario { get; set; }

        [Required(ErrorMessage = "Contraseña es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 30.")]
        public string? password_usuario { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        [StringLength(10, ErrorMessage = "Logitud máxima 10")]
        public string? estado_usuario { get; set; }

        public string? fecha_creacion_usuario { get; set; }
    }
}
