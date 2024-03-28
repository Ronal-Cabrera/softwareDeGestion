using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace softwareDeGestión.Models.Usuarios
{
    public class Login
    {
        public Login()
        {

        }

        [Required(ErrorMessage = "Usuario es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 30.")]
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "Contraseña es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Logitud minima 3, máxima 30.")]
        public string? Contra { get; set; }


    }

}
