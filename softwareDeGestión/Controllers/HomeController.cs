using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using softwareDeGestión.Models;
using softwareDeGestión.Models.Usuarios;
using System.Diagnostics;
using System.Reflection;


using Microsoft.AspNetCore.Http;




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

        Login verificar = new Login();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Login datos)
        {
            if (ModelState.IsValid)
            {
                string? usuario = datos.Usuario ?? string.Empty;
                string contra = datos.Contra ?? string.Empty;

                // Crear una instancia del servicio PasswordHasher
                IPasswordHasher<object> passwordHasher = new PasswordHasher<object>();


                string? respuesta = verificar.VerificarUsuario(usuario);
                if (respuesta != null)
                {

                    // Verificar la contraseña encriptada
                    var resultado = passwordHasher.VerifyHashedPassword("", respuesta, contra);

                    // El resultado indica si la verificación fue exitosa
                    if (resultado == PasswordVerificationResult.Success)
                    {
                        if (_httpContextAccessor.HttpContext != null)
                        {
                            _httpContextAccessor.HttpContext.Session.SetString("UsuarioActual", usuario);
                        }
                        return RedirectToAction("Privacy", "Home");
                    }

                }

            }

            return View("Index");
        }

        public IActionResult CerrarSesion()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                // Eliminar la variable de sesión
                _httpContextAccessor.HttpContext.Session.Remove("UsuarioActual");
            }

            return RedirectToAction("Privacy", "Home");
        }


        public IActionResult Privacy()
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
