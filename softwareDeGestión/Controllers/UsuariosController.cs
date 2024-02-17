using Microsoft.AspNetCore.Mvc;
using softwareDeGestión.Models;
using System.Data.SqlClient;
using System.Data;
using softwareDeGestión.Models.Usuarios;

namespace softwareDeGestión.Controllers
{

    public class UsuariosController : Controller
    {
        readonly ConexionDB conectar = new();

        public IActionResult Index()
        {
            try
            {
                string query = "select * from usuarios";
                conectar.InicioConexion();

                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                SqlDataAdapter informacionPE = new SqlDataAdapter();
                informacionPE.SelectCommand = comando;

                DataTable tablaPE = new DataTable();
                informacionPE.Fill(tablaPE);


                conectar.InicioDesconexion();

                return View(tablaPE);
            }
            catch (Exception)
            {
                return View();
            }
        }


        public IActionResult Empleados()
        {
            try
            {
                string query = "select * from empleados";
                conectar.InicioConexion();

                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                SqlDataAdapter informacionPE = new SqlDataAdapter();
                informacionPE.SelectCommand = comando;

                DataTable tablaPE = new DataTable();
                informacionPE.Fill(tablaPE);


                conectar.InicioDesconexion();

                return View(tablaPE);
            }
            catch (Exception)
            {
                return View();
            }
        }

        public IActionResult NuevoEmpleado()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardaremp(Empleados datos)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    conectar.InicioConexion();
                    DateTime fechaActual = DateTime.Now;
                    string query = "INSERT INTO empleados (nombres_empleado, apellidos_empleado, telefono_empleado, direccion_empleado, fecha_creacion_empleado) VALUES (@name, @lastname, @tel, @dire, @fecha)";
                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@name", datos.nombres_empleado);
                    cmd.Parameters.AddWithValue("@lastname", datos.apellidos_empleado);
                    cmd.Parameters.AddWithValue("@tel", datos.telefono_empleado);
                    cmd.Parameters.AddWithValue("@dire", datos.direccion_empleado);
                    cmd.Parameters.AddWithValue("@fecha", fechaActual);
                    cmd.ExecuteNonQuery();


                    conectar.InicioDesconexion();

                
                }
                catch (Exception) 
                {

                    ModelState.AddModelError(string.Empty, "Error al guardar los datos.");
                   
                }
            }

            return RedirectToAction("Empleados", "Usuarios");

        }


        public IActionResult NuevoUsuario()
        {
            return View();
        }


    }
}
