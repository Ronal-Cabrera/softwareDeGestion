using Microsoft.AspNetCore.Mvc;
using softwareDeGestión.Models.Usuarios;
using System.Data.SqlClient;
using System.Data;
using softwareDeGestión.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity;

namespace softwareDeGestión.Controllers
{
    public class PacienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IConfiguration _configuration;

        public PacienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //Conexion a la base de Datos
        readonly ConexionDB conectar = new();



        //--------------------//-----------------------//
        //Cargar vista y lista PACIENTES
        //-------------------//-----------------------//
        public IActionResult Pacientes()
        {
            try
            {
                string query = "select * from pacientes";
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

        //--------------------//-----------------------//
        //Cargar vista FORMULARIO nuevo PACIENTE
        //-------------------//-----------------------//
        public IActionResult NuevoPaciente()
        {
            return View();
        }

        //--------------------//-----------------------//
        //Cargar vista FORMULARIO editar PACIENTE
        //-------------------//-----------------------//
        public IActionResult EditarPaciente(int id)
        {
            List<List<string>> miArray = new List<List<string>>();

            conectar.InicioConexion();
            try
            {
                string query = "select * from pacientes WHERE PacienteID = @id";
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    reader.Read();

                    string cod = reader["PacienteID"]?.ToString() ?? string.Empty;
                    string nombre = reader["Nombre"]?.ToString() ?? string.Empty;
                    string apellido = reader["Apellido"]?.ToString() ?? string.Empty;
                    string edad = reader["Edad"]?.ToString() ?? string.Empty;
                    string genero = reader["Genero"]?.ToString() ?? string.Empty;
                    string direccion = reader["Direccion"]?.ToString() ?? string.Empty;
                    string tel = reader["Telefono"]?.ToString() ?? string.Empty;

                    List<string> nuevaFila = new List<string>
            {
                cod,
                nombre,
                apellido,
                edad,
                genero,
                direccion,
                tel
            };

                    miArray.Add(nuevaFila);

                }

                ViewBag.MiArray = miArray;
                Console.WriteLine(miArray);
            }
            catch (Exception)
            {
                Console.WriteLine("Error al cargar para editar.");
            }
            conectar.InicioDesconexion();
            return View();
        }

        //--------------------//-----------------------//
        //Guardar datos de FORMULARIO nuevo PACIENTE
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardarpaciente(Paciente datos)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    DateTime fechaActual = DateTime.Now;
                    string query = "INSERT INTO pacientes (Nombre, Apellido, Edad, Genero, Direccion,Telefono,FechaIngreso) VALUES (@name, @lastname, @edad, @genero, @dire, @tel, @fecha)";
                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@name", datos.Nombre);
                    cmd.Parameters.AddWithValue("@lastname", datos.Apellido);
                    cmd.Parameters.AddWithValue("@edad", datos.Edad);
                    cmd.Parameters.AddWithValue("@genero", datos.Genero);
                    cmd.Parameters.AddWithValue("@dire", datos.Direccion);
                    cmd.Parameters.AddWithValue("@tel", datos.Telefono);
                    cmd.Parameters.AddWithValue("@fecha", fechaActual);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al guardar los datos.");
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("Pacientes", "Usuarios");

        }

        //--------------------//-----------------------//
        //Guardar datos de FORMULARIO nuevo EMPLEADO
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardarupdatepaci(Paciente datos)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    string query = "UPDATE pacientes SET Nombre = @name, Apellido = @lastname, Edad = @edad, Genero = @genero, " +
                        "Direccion = @dire, Telefono = @tel  WHERE PacienteID = @id";

                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@name", datos.Nombre);
                    cmd.Parameters.AddWithValue("@lastname", datos.Apellido);
                    cmd.Parameters.AddWithValue("@edad", datos.Edad);
                    cmd.Parameters.AddWithValue("@genero", datos.Genero);
                    cmd.Parameters.AddWithValue("@dire", datos.Direccion);
                    cmd.Parameters.AddWithValue("@tel", datos.Telefono);
                    cmd.Parameters.AddWithValue("@id", datos.PacienteID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al actualizar los datos.");
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("Pacientes", "Usuarios");

        }

        //--------------------//-----------------------//
        //Eliminar Paciente
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Eliminarpaci(int id)
        {
            conectar.InicioConexion();
            try
            {
                string query = "DELETE FROM pacientes WHERE PacienteID = @id";
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Error al Eliminar.");
            }

            conectar.InicioDesconexion();

            return RedirectToAction("Pacientes", "Usuarios"); // Puedes redirigir a la acción Index u otra página según tu aplicación.
        }
    }
}
