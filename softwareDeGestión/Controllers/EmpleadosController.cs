using Microsoft.AspNetCore.Mvc;
using softwareDeGestión.Models;
using System.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity;
using softwareDeGestión.Models.Empleados;

namespace softwareDeGestión.Controllers
{
    public class EmpleadosController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        private readonly IConfiguration _configuration;

        public EmpleadosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        //Conexion a la base de Datos
        readonly ConexionDB conectar = new();


        //--------------------//-----------------------//
        //Cargar vista y lista Empleados
        //-------------------//-----------------------//
        public IActionResult Empleados(int? pagina)
        {
            int numeroDePagina = pagina ?? 1;
            int registrosPorPagina = 5, totalPaginas = 0, total = 0;

            try
            {
                string queryTotal = "select COUNT(*) as total from empleados";
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

                string query = "SELECT * FROM empleados ORDER BY EmpleadoID OFFSET " + indicador_fila + " ROWS FETCH NEXT " + registrosPorPagina + " ROWS ONLY";

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

        //--------------------//-----------------------//
        //Cargar vista FORMULARIO nuevo EMPLEADO
        //-------------------//-----------------------//
        public IActionResult NuevoEmpleado()
        {
            return View();
        }

        //--------------------//-----------------------//
        //Cargar vista FORMULARIO editar EMPLEADO
        //-------------------//-----------------------//
        public IActionResult EditarEmpleado(int id)
        {
            List<List<string>> miArray = new List<List<string>>();

            conectar.InicioConexion();
            try
            {
                string query = "select * from empleados WHERE EmpleadoID = @id";
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    reader.Read();

                    string cod = reader["EmpleadoID"]?.ToString() ?? string.Empty;
                    string nombre = reader["nombres_empleado"]?.ToString() ?? string.Empty;
                    string apellido = reader["apellidos_empleado"]?.ToString() ?? string.Empty;
                    string tel = reader["telefono_empleado"]?.ToString() ?? string.Empty;
                    string direccion = reader["direccion_empleado"]?.ToString() ?? string.Empty;

                    List<string> nuevaFila = new List<string>
                    {
                        cod,
                        nombre,
                        apellido,
                        tel,
                        direccion
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
        //Guardar datos de FORMULARIO nuevo EMPLEADO
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardaremp(Empleados datos)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    DateTime fechaActual = DateTime.Now;
                    string query = "INSERT INTO empleados (nombres_empleado, apellidos_empleado, telefono_empleado, direccion_empleado, fecha_creacion_empleado) VALUES (@name, @lastname, @tel, @dire, @fecha)";
                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@name", datos.nombres_empleado);
                    cmd.Parameters.AddWithValue("@lastname", datos.apellidos_empleado);
                    cmd.Parameters.AddWithValue("@tel", datos.telefono_empleado);
                    cmd.Parameters.AddWithValue("@dire", datos.direccion_empleado);
                    cmd.Parameters.AddWithValue("@fecha", fechaActual);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al guardar los datos.");
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("Empleados", "Usuarios");

        }

        //--------------------//-----------------------//
        //Guardar datos de FORMULARIO nuevo EMPLEADO
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardarupdateemp(Empleados datos)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    string query = "UPDATE empleados SET nombres_empleado = @name, apellidos_empleado = @lastname, telefono_empleado = @tel, direccion_empleado = @dire WHERE EmpleadoID = @id";

                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@name", datos.nombres_empleado);
                    cmd.Parameters.AddWithValue("@lastname", datos.apellidos_empleado);
                    cmd.Parameters.AddWithValue("@tel", datos.telefono_empleado);
                    cmd.Parameters.AddWithValue("@dire", datos.direccion_empleado);
                    cmd.Parameters.AddWithValue("@id", datos.EmpledoID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al actualizar los datos.");
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("Empleados", "Usuarios");

        }

        //--------------------//-----------------------//
        //Eliminar Empleado
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Eliminaremp(int id)
        {
            conectar.InicioConexion();
            try
            {
                string query = "DELETE FROM empleados WHERE EmpleadoID = @id";
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Error al Eliminar.");
            }

            conectar.InicioDesconexion();

            return RedirectToAction("Empleados", "Usuarios"); // Puedes redirigir a la acción Index u otra página según tu aplicación.
        }

    }
}
