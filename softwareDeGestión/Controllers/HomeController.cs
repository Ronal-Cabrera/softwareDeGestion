using Microsoft.AspNetCore.Mvc;
using softwareDeGestión.Models;
using softwareDeGestión.Models.Usuarios;
using System.Diagnostics;
using System.Reflection;
//using Microsoft.AspNetCore.Identity;


namespace softwareDeGestión.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //private readonly IPasswordHasher<object> _passwordHasher;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_passwordHasher = passwordHasher;
        }

        Login verificar = new Login();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Login datos)
        {/*
            if (ModelState.IsValid)
            {
                string? usuario = datos.Usuario;
                string? contra = datos.Contra;


                string? respuesta = verificar.VerificarUsuario(usuario);
                if (respuesta != null)
                {
                    var tempObject = new object();

                    // Verificar la contraseña encriptada
                    var resultado = _passwordHasher.VerifyHashedPassword(tempObject, respuesta, contra);

                    // El resultado indica si la verificación fue exitosa
                    if (resultado == PasswordVerificationResult.Success)
                    {
                        return RedirectToAction("Privacy", "Home");
                    }

                }

            }*/
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
