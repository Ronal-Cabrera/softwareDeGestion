using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using softwareDeGestión.Models;
using softwareDeGestión.Models.Usuarios;
using System.Diagnostics;
using System.Reflection;


using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;




namespace softwareDeGestión.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        readonly ConexionDB conectar = new();

        public IActionResult Index()
        {
            string? usuarioActual = null;
            if (_httpContextAccessor.HttpContext != null)
            {
                // Recuperar el nombre de usuario de la sesión
                usuarioActual = _httpContextAccessor.HttpContext.Session.GetString("UsuarioActual");
               
            }

            if (usuarioActual == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Pacientes", "Paciente");
            }
 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Login datos)
        {
            if (ModelState.IsValid)
            {
                string? usuario = datos.Usuario ?? string.Empty;
                string? contra = datos.Contra ?? string.Empty;

                string? Vcontra = "", Vusuario = "", Vid = "", Vrol = "";

                // Crear una instancia del servicio PasswordHasher
                IPasswordHasher<object> passwordHasher = new PasswordHasher<object>();

                conectar.InicioConexion();
                string query = "SELECT codigo_usuario, nombre_usuario, password_usuario, rol_usuario FROM usuarios WHERE nombre_usuario = @Username";
                using (SqlCommand command = new(query, conectar.conectar))
                {
                    command.Parameters.AddWithValue("@Username", usuario);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Vcontra = reader["password_usuario"].ToString();
                            Vusuario = reader["nombre_usuario"].ToString();
                            Vid = reader["codigo_usuario"].ToString();
                            Vrol = reader["rol_usuario"].ToString();
                            // Aquí puedes usar el valor del password_usuario como lo necesites
                        }
                    }
                }
                conectar.InicioDesconexion();


                if (Vcontra != null)
                {

                    // Verificar la contraseña encriptada
                    //var resultado = passwordHasher.VerifyHashedPassword("", respuesta, contra);
                    // Verificación de contraseñas
                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(contra, Vcontra);

                    // El resultado indica si la verificación fue exitosa
                    if (isPasswordValid)
                    {
                        if (_httpContextAccessor.HttpContext != null && Vusuario != null && Vrol != null && Vid != null)
                        {
                            _httpContextAccessor.HttpContext.Session.SetString("UsuarioActual", Vusuario);
                            _httpContextAccessor.HttpContext.Session.SetString("RolActual", Vrol);
                            _httpContextAccessor.HttpContext.Session.SetString("IdActual", Vid);
                        }
                        return RedirectToAction("Pacientes", "Paciente");
                    }

                }

            }

            return View("Index");
        }

        public string? VerificarUsuario(string? Usuario)
        {
            string? respuesta = null;

            conectar.InicioConexion();
            try
            {
                string query = "SELECT codigo_usuario, nombre_usuario, password_usuario, rol_usuario FROM usuarios WHERE nombre_usuario = @Username";
                using (SqlCommand command = new(query, conectar.conectar))
                {
                    command.Parameters.AddWithValue("@Username", Usuario);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            respuesta = reader["password_usuario"].ToString();
                            // Aquí puedes usar el valor del password_usuario como lo necesites
                        }
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
       



        public IActionResult CerrarSesion()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                // Eliminar la variable de sesión
                _httpContextAccessor.HttpContext.Session.Remove("UsuarioActual");
                _httpContextAccessor.HttpContext.Session.Remove("RolActual");
                _httpContextAccessor.HttpContext.Session.Remove("IdActual");
            }

            return RedirectToAction("Index", "Home");
        }






        public IActionResult Dashboard()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                // Recuperar el nombre de usuario de la sesión
                string? usuarioActual = _httpContextAccessor.HttpContext.Session.GetString("UsuarioActual");
                ViewData["UsuarioActual"] = usuarioActual;
            }

            
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
