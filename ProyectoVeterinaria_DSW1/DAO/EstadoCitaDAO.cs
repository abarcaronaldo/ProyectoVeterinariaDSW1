using Microsoft.Data.SqlClient;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using System.Data;

namespace ProyectoVeterinaria_DSW1.DAO
{
    public class EstadoCitaDAO : IEstadoCita
    {
        string cadena = (new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).GetConnectionString("cn") ?? "";
        public List<EstadoCita> ListarEstados()
        {
            List<EstadoCita> temporal = new List<EstadoCita>();
            string spName = "sp_ListarEstadosCita";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(spName, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            temporal.Add(new EstadoCita
                            {
                                IdEstado = (int)dr["IdEstado"],
                                Estado = dr["Estado"].ToString()
                            });
                        }
                    }
                }
            }
            return temporal;
        }
    }
}
