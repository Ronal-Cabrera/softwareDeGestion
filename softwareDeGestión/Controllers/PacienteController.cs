using Microsoft.AspNetCore.Mvc;
using softwareDeGestión.Models.Usuarios;
using System.Data.SqlClient;
using System.Data;
using softwareDeGestión.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity;
using softwareDeGestión.Models.Paciente;
using softwareDeGestión.Models.Vistas;

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
        public IActionResult Pacientes(int? pagina)
        {

            if (HttpContext.Session.GetString("UsuarioActual") != null)
            {
                // Recuperar el nombre de usuario de la sesión
                string? usuarioActual = HttpContext.Session.GetString("UsuarioActual");
                ViewData["UsuarioActual"] = usuarioActual;
                ViewData["RolActual"] = HttpContext.Session.GetString("RolActual");

                int numeroDePagina = pagina ?? 1;
                int registrosPorPagina = 5, totalPaginas = 0, total = 0;

                try
                {

                    string queryTotal = "select COUNT(*) as total from pacientes";
                    conectar.InicioConexion();
                    SqlCommand comando2 = new SqlCommand(queryTotal, conectar.conectar);
                    using (SqlDataReader reader = comando2.ExecuteReader())
                    {
                        reader.Read();
                        total = Convert.ToInt32(reader["total"]);
                    }
                    conectar.InicioDesconexion();


                    if (total > registrosPorPagina)
                    {
                        /////total paginas
                        double numero_total_productos = total;
                        double resultado_divicion = numero_total_productos / 5.0;
                        double resultadoRedondeado = Math.Ceiling(resultado_divicion);
                        totalPaginas = (int)resultadoRedondeado;
                    }
                    else
                    {
                        totalPaginas = 1;
                    }

                    int? indicador_fila = registrosPorPagina * (numeroDePagina - 1);

                    string query = "select * from pacientes ORDER BY PacienteID OFFSET " + indicador_fila + " ROWS FETCH NEXT " + registrosPorPagina + " ROWS ONLY";
                    conectar.InicioConexion();

                    SqlCommand comando = new SqlCommand(query, conectar.conectar);
                    SqlDataAdapter informacionPE = new SqlDataAdapter();
                    informacionPE.SelectCommand = comando;

                    DataTable tablaPE = new DataTable();
                    informacionPE.Fill(tablaPE);


                    conectar.InicioDesconexion();
                    ViewBag.PaginaActual = numeroDePagina;
                    ViewBag.TotalPaginas = totalPaginas;

                    return View(tablaPE);
                }
                catch (Exception)
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //--------------------//-----------------------//
        //Cargar vista FORMULARIO nuevo PACIENTE
        //-------------------//-----------------------//
        public IActionResult NuevoPaciente()
        {
            if (HttpContext.Session.GetString("UsuarioActual") != null)
            {
                // Recuperar el nombre de usuario de la sesión
                string? usuarioActual = HttpContext.Session.GetString("UsuarioActual");
                ViewData["UsuarioActual"] = usuarioActual;
                ViewData["RolActual"] = HttpContext.Session.GetString("RolActual");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //--------------------//-----------------------//
        //Cargar vista FORMULARIO editar PACIENTE
        //-------------------//-----------------------//
        public IActionResult EditarPaciente(int id)
        {
            if (HttpContext.Session.GetString("UsuarioActual") != null)
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //--------------------//-----------------------//
        //Guardar datos de FORMULARIO nuevo PACIENTE
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardarpaciente(Paciente datos)
        {
            int pacienteID = 0;

            if (ModelState.IsValid)
            {
               
                conectar.InicioConexion();
                try
                {
                    DateTime fechaActual = DateTime.Now;
                    string query = "INSERT INTO pacientes (Nombre, Apellido, Edad, Genero, Direccion,Telefono,FechaIngreso) VALUES (@name, @lastname, @edad, @genero, @dire, @tel, @fecha); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@name", datos.Nombre);
                    cmd.Parameters.AddWithValue("@lastname", datos.Apellido);
                    cmd.Parameters.AddWithValue("@edad", datos.Edad);
                    cmd.Parameters.AddWithValue("@genero", datos.Genero);
                    cmd.Parameters.AddWithValue("@dire", datos.Direccion);
                    cmd.Parameters.AddWithValue("@tel", datos.Telefono);
                    cmd.Parameters.AddWithValue("@fecha", fechaActual);
                    //cmd.ExecuteNonQuery();

                    pacienteID = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception)
                {
                    Console.WriteLine("Error al guardar Paciente.");
                }
                conectar.InicioDesconexion();

                conectar.InicioConexion();
                try
                {
                    DateTime fechaActual = DateTime.Now;
                    string query = "INSERT INTO factores_riesgo (PacienteID) VALUES (@id);";
                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@id", pacienteID);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    Console.WriteLine("Error al guardar Riesgos.");
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("DetallesPaciente", "Detallesvistas", new { id = pacienteID });

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

            return RedirectToAction("DetallesPaciente", "Detallesvistas", new { id = datos.PacienteID });

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

            return RedirectToAction("Pacientes", "Paciente"); // Puedes redirigir a la acción Index u otra página según tu aplicación.
        }














        //--------------------//-----------------------//
        //Cargar vista FORMULARIO editar PACIENTE
        //-------------------//-----------------------//
        public IActionResult EditarFactoresRiesgo(int id)
        {
            if (HttpContext.Session.GetString("UsuarioActual") != null)
            {
                List<List<string>> miArray = new List<List<string>>();

                conectar.InicioConexion();
                try
                {
                    string query = "select * from factores_riesgo WHERE PacienteID = @id";
                    SqlCommand comando = new SqlCommand(query, conectar.conectar);
                    comando.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        reader.Read();

                        string FactorID = reader["FactorID"]?.ToString() ?? string.Empty;
                        string HistorialFamiliarDiabetes = reader["HistorialFamiliarDiabetes"]?.ToString() ?? string.Empty;
                        string ActividadFisica = reader["ActividadFisica"]?.ToString() ?? string.Empty;
                        string HabitosAlimenticios = reader["HabitosAlimenticios"]?.ToString() ?? string.Empty;
                        string NivelesEstres = reader["NivelesEstres"]?.ToString() ?? string.Empty;
                        string OtrosFactores = reader["OtrosFactores"]?.ToString() ?? string.Empty;

                        List<string> nuevaFila = new List<string>
                        {
                            FactorID,
                            HistorialFamiliarDiabetes,
                            ActividadFisica,
                            HabitosAlimenticios,
                            NivelesEstres,
                            OtrosFactores
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

       
        //--------------------//-----------------------//
        //Guardar datos de FORMULARIO nuevo EMPLEADO
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardarupdateriesgo(FactoresRiesgo datos)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    string query = "UPDATE factores_riesgo SET HistorialFamiliarDiabetes = @HistorialFamiliarDiabetes, ActividadFisica = @ActividadFisica, HabitosAlimenticios = @HabitosAlimenticios, NivelesEstres = @NivelesEstres, " +
                        "OtrosFactores = @OtrosFactores WHERE PacienteID = @id";

                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@HistorialFamiliarDiabetes", datos.HistorialFamiliarDiabetes);
                    cmd.Parameters.AddWithValue("@ActividadFisica", datos.ActividadFisica);
                    cmd.Parameters.AddWithValue("@HabitosAlimenticios", datos.HabitosAlimenticios);
                    cmd.Parameters.AddWithValue("@NivelesEstres", datos.NivelesEstres);
                    cmd.Parameters.AddWithValue("@OtrosFactores", datos.OtrosFactores);
                    cmd.Parameters.AddWithValue("@id", datos.PacienteID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al actualizar los datos." + e.Message);
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("DetallesPaciente", "Detallesvistas", new { id = datos.PacienteID });

        }
    }
}
