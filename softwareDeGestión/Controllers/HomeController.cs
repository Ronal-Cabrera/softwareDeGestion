using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using softwareDeGestión.Models;
using softwareDeGestión.Models.Usuarios;
using System.Diagnostics;
using System.Reflection;



namespace softwareDeGestión.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

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
                string? usuario = datos.Usuario;
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
                        return RedirectToAction("Privacy", "Home");
                    }

                }

            }

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
