using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using softwareDeGestión.Models;
using softwareDeGestión.Models.Vistas;
using System.Data;
using System.Data.SqlClient;


using Microsoft.AspNetCore.Http;
using softwareDeGestión.Models.Paciente;


namespace softwareDeGestión.Controllers
{
    public class DetallesvistasController : Controller
    {

        private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public DetallesvistasController(IConfiguration configuration)
        {
            _configuration = configuration;
            //_httpContextAccessor = httpContextAccessor;
        }
        ConexionDB conectar = new ConexionDB();


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DetallesPaciente(int? id)
        {
            try
            {
                var resultados = new List<Paciente>();
                string query = "SELECT * FROM pacientes WHERE PacienteID = @id";
                conectar.InicioConexion();
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    reader.Read();
                    
                        // Mapea los datos a tu modelo de entidad
                        var fila = new Paciente
                        {
                            PacienteID = Convert.ToInt32(reader["PacienteID"]),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Edad = reader["Edad"].ToString(),
                            Genero = reader["Genero"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            FechaIngreso = reader["FechaIngreso"].ToString()

                        };

                        resultados.Add(fila);
                        Console.WriteLine(fila);
                    
                }

                conectar.InicioDesconexion();
                ViewBag.DatosTabla1 = resultados;
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }



        public IActionResult ResultadosLaboratorio(int? id, int? pagina, string? name)
        {
            int numeroDePagina = pagina ?? 1;
            int registrosPorPagina = 5, totalPaginas = 0, total = 0;



            var resultados = new List<ResultadosLaboratorio>();
            try
            {

                string queryTotal = "select COUNT(*) as total from resultados_laboratorio WHERE PacienteID = @id;";
                conectar.InicioConexion();
                SqlCommand comando2 = new SqlCommand(queryTotal, conectar.conectar);
                comando2.Parameters.AddWithValue("@id", id);
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

                string query = "SELECT * FROM resultados_laboratorio WHERE PacienteID = @id ORDER BY ResultadoID OFFSET " + indicador_fila + " ROWS FETCH NEXT " + registrosPorPagina + " ROWS ONLY;";
                conectar.InicioConexion();
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Mapea los datos a tu modelo de entidad
                        var fila = new ResultadosLaboratorio
                        {
                            ResultadoID = Convert.ToInt32(reader["ResultadoID"]),
                            FechaControl = reader["FechaControl"].ToString(),
                            NivelGlucosa = Convert.ToSingle(reader["NivelGlucosa"]),
                            Comentarios = reader["Comentarios"].ToString(),
                            OtrosResultados = reader["OtrosResultados"].ToString()

                        };

                        resultados.Add(fila);
                        Console.WriteLine(fila);
                    }
                }

                conectar.InicioDesconexion();
                ViewBag.DatosTabla1 = resultados;

                ViewBag.PaginaActual = numeroDePagina;
                ViewBag.TotalPaginas = totalPaginas;
                ViewBag.name = name;
                ViewBag.id = id;


                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }



        public IActionResult RegistroAlimentacion(int? id, int? pagina, string? name)
        {
            int numeroDePagina = pagina ?? 1;
            int registrosPorPagina = 5, totalPaginas = 0, total = 0;



            var resultados = new List<RegistroAlimentacion>();
            try
            {

                string queryTotal = "select COUNT(*) as total from registro_alimentacion WHERE PacienteID = @id;";
                conectar.InicioConexion();
                SqlCommand comando2 = new SqlCommand(queryTotal, conectar.conectar);
                comando2.Parameters.AddWithValue("@id", id);
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

                string query = "SELECT * FROM registro_alimentacion WHERE PacienteID = @id ORDER BY AlimentacionID OFFSET " + indicador_fila + " ROWS FETCH NEXT " + registrosPorPagina + " ROWS ONLY;";
                conectar.InicioConexion();
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Mapea los datos a tu modelo de entidad
                        var fila = new RegistroAlimentacion
                        {
                            AlimentacionID = Convert.ToInt32(reader["AlimentacionID"]),
                            FechaRegistro = reader["FechaRegistro"].ToString(),
                            DescripcionComidas = reader["DescripcionComidas"].ToString(),
                            ConteoCarbohidratos = reader["ConteoCarbohidratos"].ToString(),
                            ObservacionesAlimentacion = reader["ObservacionesAlimentacion"].ToString()

                        };

                        resultados.Add(fila);
                        Console.WriteLine(fila);
                    }
                }

                conectar.InicioDesconexion();
                ViewBag.DatosTabla1 = resultados;

                ViewBag.PaginaActual = numeroDePagina;
                ViewBag.TotalPaginas = totalPaginas;
                ViewBag.name = name;
                ViewBag.id = id;


                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }


        public IActionResult RegistroActividadFisica(int? id, int? pagina, string? name)
        {
            int numeroDePagina = pagina ?? 1;
            int registrosPorPagina = 5, totalPaginas = 0, total = 0;



            var resultados = new List<RegistroActividadFisica>();
            try
            {

                string queryTotal = "select COUNT(*) as total from registro_actividad_fisica WHERE PacienteID = @id ;";
                conectar.InicioConexion();
                SqlCommand comando2 = new SqlCommand(queryTotal, conectar.conectar);
                comando2.Parameters.AddWithValue("@id", id);
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

                string query = "SELECT * FROM registro_actividad_fisica WHERE PacienteID = @id ORDER BY ActividadID OFFSET " + indicador_fila + " ROWS FETCH NEXT " + registrosPorPagina + " ROWS ONLY;";
                conectar.InicioConexion();
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Mapea los datos a tu modelo de entidad
                        var fila = new RegistroActividadFisica
                        {
                            ActividadID = Convert.ToInt32(reader["ActividadID"]),
                            FechaRegistro = reader["FechaRegistro"].ToString(),
                            TipoActividad = reader["TipoActividad"].ToString(),
                            DuracionActividad = reader["DuracionActividad"].ToString(),
                            IntensidadActividad = reader["IntensidadActividad"].ToString()

                        };

                        resultados.Add(fila);
                        Console.WriteLine(fila);
                    }
                }

                conectar.InicioDesconexion();
                ViewBag.DatosTabla1 = resultados;

                ViewBag.PaginaActual = numeroDePagina;
                ViewBag.TotalPaginas = totalPaginas;
                ViewBag.name = name;
                ViewBag.id = id;


                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }



        public IActionResult Prescripciones(int? id, int? pagina, string? name)
        {
            int numeroDePagina = pagina ?? 1;
            int registrosPorPagina = 5, totalPaginas = 0, total = 0;



            var resultados = new List<Prescripciones>();
            try
            {

                string queryTotal = "select COUNT(*) as total from prescripciones WHERE PacienteID = @id ;";
                conectar.InicioConexion();
                SqlCommand comando2 = new SqlCommand(queryTotal, conectar.conectar);
                comando2.Parameters.AddWithValue("@id", id);
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

                string query = "SELECT * FROM prescripciones P INNER JOIN medicamentos M ON P.MedicamentoID = M.MedicamentoID WHERE P.PacienteID = @id ORDER BY P.PrescripcionID OFFSET " + indicador_fila + " ROWS FETCH NEXT " + registrosPorPagina + " ROWS ONLY;";
                conectar.InicioConexion();
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Mapea los datos a tu modelo de entidad
                        var fila = new Prescripciones
                        {
                            PrescripcionID = Convert.ToInt32(reader["PrescripcionID"]),
                            NombreMedicamento = reader["NombreMedicamento"].ToString(),
                            FechaPrescripcion = reader["FechaPrescripcion"].ToString(),
                            DosisPrescrita = reader["DosisPrescrita"].ToString(),
                            DuracionPrescripcion = reader["DuracionPrescripcion"].ToString(),
                            NotasAdicionales = reader["NotasAdicionales"].ToString()

                        };

                        resultados.Add(fila);
                        Console.WriteLine(fila);
                    }
                }

                conectar.InicioDesconexion();
                ViewBag.DatosTabla1 = resultados;

                ViewBag.PaginaActual = numeroDePagina;
                ViewBag.TotalPaginas = totalPaginas;
                ViewBag.name = name;
                ViewBag.id = id;


                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }




        public IActionResult DecisionesClinicas(int? id, int? pagina, string? name)
        {
            int numeroDePagina = pagina ?? 1;
            int registrosPorPagina = 5, totalPaginas = 0, total = 0;



            var resultados = new List<DecisionesClinicas>();
            try
            {

                string queryTotal = "select COUNT(*) as total from decisiones_clinicas WHERE PacienteID = @id ;";
                conectar.InicioConexion();
                SqlCommand comando2 = new SqlCommand(queryTotal, conectar.conectar);
                comando2.Parameters.AddWithValue("@id", id);
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

                string query = "SELECT * FROM decisiones_clinicas WHERE PacienteID = @id ORDER BY DecisionID OFFSET " + indicador_fila + " ROWS FETCH NEXT " + registrosPorPagina + " ROWS ONLY;";
                conectar.InicioConexion();
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Mapea los datos a tu modelo de entidad
                        var fila = new DecisionesClinicas
                        {
                            DecisionID = Convert.ToInt32(reader["DecisionID"]),
                            FechaDecision = reader["FechaDecision"].ToString(),
                            TratamientosRecomendados = reader["TratamientosRecomendados"].ToString(),
                            MedicamentosRecetados = reader["MedicamentosRecetados"].ToString(),
                            CambiosEstiloVida = reader["CambiosEstiloVida"].ToString(),
                            SeguimientoProximasCitas = reader["SeguimientoProximasCitas"].ToString()

                        };

                        resultados.Add(fila);
                        Console.WriteLine(fila);
                    }
                }

                conectar.InicioDesconexion();
                ViewBag.DatosTabla1 = resultados;

                ViewBag.PaginaActual = numeroDePagina;
                ViewBag.TotalPaginas = totalPaginas;
                ViewBag.name = name;
                ViewBag.id = id;


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
        public IActionResult Medicamentos(int? pagina)
        {
            /*
            if (_httpContextAccessor.HttpContext != null)
            {
                string? usuarioActual = _httpContextAccessor.HttpContext.Session.GetString("UsuarioActual");
                ViewData["UsuarioActual"] = usuarioActual;
            }*/

            int numeroDePagina = pagina ?? 1;
            int registrosPorPagina = 5, totalPaginas = 0, total = 0;

            try
            {
                string queryTotal = "select COUNT(*) as total from medicamentos";
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

                string query = "SELECT * FROM medicamentos ORDER BY MedicamentoID OFFSET " + indicador_fila + " ROWS FETCH NEXT " + registrosPorPagina + " ROWS ONLY";

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
        public IActionResult NuevoMedicamento()
        {
            return View();
        }

        //--------------------//-----------------------//
        //Cargar vista FORMULARIO editar EMPLEADO
        //-------------------//-----------------------//
        public IActionResult EditarMedicamento(int id)
        {
            List<List<string>> miArray = new List<List<string>>();

            conectar.InicioConexion();
            try
            {
                string query = "select * from medicamentos WHERE MedicamentoID = @id";
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    reader.Read();

                    string MedicamentoID = reader["MedicamentoID"]?.ToString() ?? string.Empty;
                    string NombreMedicamento = reader["NombreMedicamento"]?.ToString() ?? string.Empty;
                    string DescripcionMedicamento = reader["DescripcionMedicamento"]?.ToString() ?? string.Empty;
                    string InstruccionesUso = reader["InstruccionesUso"]?.ToString() ?? string.Empty;
                    string EfectosSecundarios = reader["EfectosSecundarios"]?.ToString() ?? string.Empty;

                    List<string> nuevaFila = new List<string>
                    {
                        MedicamentoID,
                        NombreMedicamento,
                        DescripcionMedicamento,
                        InstruccionesUso,
                        EfectosSecundarios
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
        public IActionResult Guardarme(Medicamentos datos)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    string query = "INSERT INTO medicamentos (NombreMedicamento,DescripcionMedicamento,InstruccionesUso,EfectosSecundarios) VALUES (@NombreMedicamento,@DescripcionMedicamento,@InstruccionesUso,@EfectosSecundarios)";
                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);

                    cmd.Parameters.AddWithValue("@NombreMedicamento", datos.NombreMedicamento);
                    cmd.Parameters.AddWithValue("@DescripcionMedicamento", datos.DescripcionMedicamento);
                    cmd.Parameters.AddWithValue("@InstruccionesUso", datos.InstruccionesUso);
                    cmd.Parameters.AddWithValue("@EfectosSecundarios", datos.EfectosSecundarios);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al guardar los datos.");
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("Medicamentos", "DetallesVistas");

        }

        //--------------------//-----------------------//
        //Guardar datos de FORMULARIO nuevo EMPLEADO
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Guardarupdateme(Medicamentos datos)
        {
            if (ModelState.IsValid)
            {
                conectar.InicioConexion();
                try
                {
                    string query = "UPDATE medicamentos SET NombreMedicamento = @NombreMedicamento, DescripcionMedicamento = @DescripcionMedicamento, InstruccionesUso = @InstruccionesUso, EfectosSecundarios = @EfectosSecundarios WHERE MedicamentoID = @id";

                    SqlCommand cmd = new SqlCommand(query, conectar.conectar);
                    cmd.Parameters.AddWithValue("@id", datos.MedicamentoID);
                    cmd.Parameters.AddWithValue("@NombreMedicamento", datos.NombreMedicamento);
                    cmd.Parameters.AddWithValue("@DescripcionMedicamento", datos.DescripcionMedicamento);
                    cmd.Parameters.AddWithValue("@InstruccionesUso", datos.InstruccionesUso);
                    cmd.Parameters.AddWithValue("@EfectosSecundarios", datos.EfectosSecundarios);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al actualizar los datos.");
                }
                conectar.InicioDesconexion();
            }

            return RedirectToAction("Medicamentos", "DetallesVistas");

        }

        //--------------------//-----------------------//
        //Eliminar Empleado
        //-------------------//-----------------------//
        [HttpPost]
        public IActionResult Eliminarme(int id)
        {
            conectar.InicioConexion();
            try
            {
                string query = "DELETE FROM medicamentos WHERE MedicamentoID = @id";
                SqlCommand comando = new SqlCommand(query, conectar.conectar);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Error al Eliminar.");
            }

            conectar.InicioDesconexion();

            return RedirectToAction("Medicamentos", "DetallesVistas"); // Puedes redirigir a la acción Index u otra página según tu aplicación.
        }


    }
}
