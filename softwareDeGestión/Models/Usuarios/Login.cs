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


        readonly ConexionDB conectar = new();
        public string? VerificarUsuario(string? Usuario)
        {
            string? respuesta = null;

            conectar.InicioConexion();
            try
            {
                string query = "SELECT password_usuario FROM usuarios WHERE nombre_usuario = @Username";
                using (SqlCommand command = new(query, conectar.conectar))
                {
                    command.Parameters.AddWithValue("@Username", Usuario);

                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        respuesta = result.ToString();
                    }
                }
            }
            catch (Exception)
            {
                respuesta = null;
            }

            conectar.InicioDesconexion();

            return respuesta;
        }
    }

}
