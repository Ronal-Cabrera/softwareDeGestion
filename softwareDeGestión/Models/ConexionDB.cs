using System.Data.SqlClient;

namespace softwareDeGestión.Models
{
    public class ConexionDB
    {
        string servidor = "LAPTOP-5ICDT4FV";
        string baseDatos = "ProyectU";  //name base de datos
        string usuario = "Administrador";
        string contra = "Administrador";

        //Variables
        string conexion = "";

        public SqlConnection conectar = new();

        public ConexionDB()
        {
            conexion = "Data source = " + servidor + "; Initial Catalog = " + baseDatos + "; user id = "
                + usuario + "; password = " + contra + ";";
            conectar.ConnectionString = conexion;
        }
        public void InicioConexion()
        {
            try
            {
                conectar.Open();
                Console.WriteLine("Conectado");
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
        public void InicioDesconexion()
        {
            try
            {
                conectar.Close();
                Console.WriteLine("Desonectado");
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
    }
}
