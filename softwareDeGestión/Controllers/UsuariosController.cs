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
        //Cargar vista y lista Empleados
        //-------------------//-----------------------//
        public IActionResult Index(int? pagina)
        {
            if (HttpContext.Session.GetString("UsuarioActual") != null && HttpContext.Session.GetString("RolActual") == "Administrador")
            {
                int numeroDePagina = pagina ?? 1;
            int registrosPorPagina = 50, totalPaginas = 0, total = 0;
            


            var resultados = new List<Usuario>();
            try
            {
                string queryTotal = "select COUNT(*) as total from usuarios";
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
                
                string query = "SELECT * FROM usuarios ORDER BY codigo_usuario OFFSET "+ indicador_fila + " ROWS FETCH NEXT "+ registrosPorPagina + " ROWS ONLY";
                conectar.InicioConexion();

                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                SqlDataAdapter informacionPE = new SqlDataAdapter();
                informacionPE.SelectCommand = comando;

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
                        Console.WriteLine(fila);
                    }
                }

                conectar.InicioDesconexion();
                ViewBag.DatosTabla1 = resultados;

                ViewBag.PaginaActual = numeroDePagina;
                ViewBag.TotalPaginas = totalPaginas;

                return View();
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
        //Cargar vista FORMULARIO nuevo USUARIO
        //-------------------//-----------------------//
        public IActionResult NuevoUsuario()
        {
            if (HttpContext.Session.GetString("UsuarioActual") != null && HttpContext.Session.GetString("RolActual") == "Administrador")
            {
                List<List<string>> miArray = new List<List<string>>();

            conectar.InicioConexion();
            try
            {
                string query = "select * from empleados LEFT JOIN usuarios ON empleados.EmpleadoID = usuarios.EmpleadoID WHERE usuarios.EmpleadoID IS NULL;";
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
            else
            {
                return RedirectToAction("Index", "Home");
            }

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
                    //IPasswordHasher<object> passwordHasher = new PasswordHasher<object>();
                    // Encriptar la contraseña
                    //string hashedPassword = passwordHasher.HashPassword("", plainPassword);
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword, BCrypt.Net.BCrypt.GenerateSalt());

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
                string query = "DELETE FROM usuarios WHERE codigo_usuario = @id";
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
            if (HttpContext.Session.GetString("UsuarioActual") != null && HttpContext.Session.GetString("RolActual") == "Administrador")
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
        //Guardar datos de FORMULARIO editar Password
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
                    //IPasswordHasher<object> passwordHasher = new PasswordHasher<object>();
                    // Encriptar la contraseña
                    //string hashedPassword = passwordHasher.HashPassword("", plainPassword);
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword, BCrypt.Net.BCrypt.GenerateSalt());

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


        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        

    }
}
