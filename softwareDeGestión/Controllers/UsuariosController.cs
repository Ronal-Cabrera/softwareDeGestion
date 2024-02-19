using Microsoft.AspNetCore.Mvc;
using softwareDeGestión.Models;
using System.Data.SqlClient;
using System.Data;
using softwareDeGestión.Models.Usuarios;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity;

namespace softwareDeGestión.Controllers
{

    public class UsuariosController : Controller
    {
        private readonly IConfiguration _configuration;

        public UsuariosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }




        //Conexion a la base de Datos
        readonly ConexionDB conectar = new();

        //--------------------//-----------------------//
        //Cargar vista y lista Usuarios
        //-------------------//-----------------------//

        public IActionResult Index()
        {

            var resultados = new List<Usuario>();

            try
            {
                string query = "select * from usuarios";
                conectar.InicioConexion();
                SqlCommand comando = new SqlCommand(query, conectar.conectar);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Mapea los datos a tu modelo de entidad
                        var fila = new Usuario
                        {
                            codigo_usuario = Convert.ToInt32(reader["codigo_usuario"]),
                            nombre_usuario = reader["nombre_usuario"].ToString(),
                            estado_usuario = reader["estado_usuario"].ToString(),
                            rol_usuario = reader["rol_usuario"].ToString(),
                            fecha_creacion_usuario = reader["fecha_creacion_usuario"].ToString()
                            
                        };

                        resultados.Add(fila);
                    }
                }

                conectar.InicioDesconexion();
                ViewBag.DatosTabla1 = resultados;
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        //--------------------//-----------------------//
        //Cargar vista y lista Empleados
        //-------------------//-----------------------//
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






















        //--------------------//-----------------------//
        //Cargar vista FORMULARIO nuevo USUARIO
        //-------------------//-----------------------//
        public IActionResult NuevoUsuario()
        {
            List<List<string>> miArray = new List<List<string>>();

            conectar.InicioConexion();
            try
            {
                string query = "select * from empleados LEFT JOIN usuarios ON empleados.EmpleadoID != usuarios.EmpleadoID";
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string cod = reader["EmpleadoID"]?.ToString() ?? string.Empty;
                        string nombre = reader["nombres_empleado"]?.ToString() ?? string.Empty;

                        miArray.Add(new List<string> { cod, nombre });
                    }
                }

                ViewBag.MiArray = miArray;
                Console.WriteLine(miArray);
            }
            catch (Exception)
            {
                Console.WriteLine("Error al cargar.");
            }
            conectar.InicioDesconexion();
            return View();

        }

        //--------------------//-----------------------//
        //Guardar datos de FORMULARIO nuevo USUARIO
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardarusu(Usuario datos)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    string plainPassword = datos.password_usuario ?? string.Empty;
                    // Crear una instancia del servicio PasswordHasher
                    IPasswordHasher<object> passwordHasher = new PasswordHasher<object>();
                    // Encriptar la contraseña
                    string hashedPassword = passwordHasher.HashPassword("", plainPassword);

                    DateTime fechaActual = DateTime.Now;
                    string query = "INSERT INTO usuarios (EmpleadoID, nombre_usuario, password_usuario, estado_usuario, rol_usuario, fecha_creacion_usuario) VALUES (@EmpleadoID, @usuario, @contra, @estado, @rol, @fecha)";
                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@EmpleadoID", datos.EmpleadoID);
                    cmd.Parameters.AddWithValue("@usuario", datos.nombre_usuario);
                    cmd.Parameters.AddWithValue("@contra", hashedPassword);
                    cmd.Parameters.AddWithValue("@estado", datos.estado_usuario);
                    cmd.Parameters.AddWithValue("@rol", datos.rol_usuario);
                    cmd.Parameters.AddWithValue("@fecha", fechaActual);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Error al guardar los datos.");
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("Index", "Usuarios");

        }

        //--------------------//-----------------------//
        //Eliminar USUARIO
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Eliminarusu(int id)
        {
            conectar.InicioConexion();
            try
            {
                string query = "DELETE FROM usuarios WHERE EmpleadoID = @id";
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Error al Eliminar.");
            }

            conectar.InicioDesconexion();

            return RedirectToAction("Index", "Usuarios"); // Puedes redirigir a la acción Index u otra página según tu aplicación.
        }

        //--------------------//-----------------------//
        //Cargar vista FORMULARIO editar EMPLEADO
        //-------------------//-----------------------//
        public IActionResult EditarUsuario(int id)
        {
            List<List<string>> miArrayUE = new List<List<string>>();

            conectar.InicioConexion();
            try
            {
                string query = "select * from usuarios WHERE codigo_usuario = @id";
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    reader.Read();

                    string cod = reader["codigo_usuario"]?.ToString() ?? string.Empty;
                    string nombre = reader["nombre_usuario"]?.ToString() ?? string.Empty;
                    string estado = reader["estado_usuario"]?.ToString() ?? string.Empty;

                    List<string> nuevaFila = new List<string>
                    {
                        cod,
                        nombre,
                        estado
                    };

                    miArrayUE.Add(nuevaFila);

                }

                ViewBag.MiArrayUE = miArrayUE;
   
            }
            catch (Exception)
            {
                Console.WriteLine("Error al cargar para editar.");
            }
            conectar.InicioDesconexion();
            return View();
        }

        //--------------------//-----------------------//
        //Guardar datos de FORMULARIO editar USUARIO
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardarupdateusuario(int id, string cambio)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    string query = "UPDATE usuarios SET nombre_usuario = @cambio WHERE codigo_usuario = @id";

                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@cambio", cambio);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al actualizar los datos.");
                }
                conectar.InicioDesconexion();
            }
       
            return RedirectToAction("Index", "Usuarios");

        }

        //--------------------//-----------------------//
        //Guardar datos de FORMULARIO editar ESTADO
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardarupdateestado(int id, string cambio)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    string query = "UPDATE usuarios SET estado_usuario = @cambio WHERE codigo_usuario = @id";

                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@cambio", cambio);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al actualizar los datos.");
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("Index", "Usuarios");

        }

        //--------------------//-----------------------//
        //Guardar datos de FORMULARIO editar USUARIO
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardarupdatepass(int id, string cambio)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    string plainPassword = cambio ?? string.Empty;
                    // Crear una instancia del servicio PasswordHasher
                    IPasswordHasher<object> passwordHasher = new PasswordHasher<object>();
                    // Encriptar la contraseña
                    string hashedPassword = passwordHasher.HashPassword("", plainPassword);


                    string query = "UPDATE usuarios SET password_usuario = @cambio WHERE codigo_usuario = @id";

                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@cambio", hashedPassword);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al actualizar los datos.");
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("Index", "Usuarios");

        }

    }
}
