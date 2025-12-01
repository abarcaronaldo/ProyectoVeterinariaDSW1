using Microsoft.Data.SqlClient;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using System.Data;

namespace ProyectoVeterinaria_DSW1.DAO
{
    public class DuenoDAO : IDueno
    {
        string cadena = (new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).GetConnectionString("cn") ?? "";

        public string actualizar(Dueno entidad)
        {
            throw new NotImplementedException();
        }

        public string agregar(Dueno entidad)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_insertar_dueno", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", entidad.idusuario);
                        cmd.Parameters.AddWithValue("@Nombre", entidad.nombre);
                        cmd.Parameters.AddWithValue("@Apellido", entidad.apellido);
                        cmd.Parameters.AddWithValue("@Telefono", entidad.telefono);
                        cmd.Parameters.AddWithValue("@Direccion", entidad.direccion);
                        cn.Open();
                        int i = cmd.ExecuteNonQuery();
                        mensaje = $"Se ha registrado {i} un nuevo usuario";
                    }
                }
            }
            catch (Exception ex) { mensaje = ex.Message; }
            return mensaje;

        }

        public Dueno buscar(int id)
        {
            throw new NotImplementedException();
        }

        public string eliminar(Dueno entidad)
        {
            throw new NotImplementedException();
        }
    }
}
