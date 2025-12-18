using Microsoft.Data.SqlClient;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using System.Data;

namespace ProyectoVeterinaria_DSW1.DAO
{
    public class HistorialDAO : IHistorial
    {
        string cadena = (new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).GetConnectionString("cn") ?? "";
        public HistorialMedico ObtenerInfoInicial(int idCita)
        {
            HistorialMedico info = new HistorialMedico();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerInfoInicial", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCita", idCita);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            info = new HistorialMedico
                            {
                                IdMascota = (int)dr["IdMascota"],
                                NombreMascota = dr["NombreMascota"].ToString()
                            };
                        }
                    }
                }
            }
            return info;
        }

        public int RegistrarAtencionMedica(HistorialMedico model)
        {
            int resultado = 0;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_RegistrarAtencionMedica", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCita", model.IdCita);
                    cmd.Parameters.AddWithValue("@IdMascota", model.IdMascota);
                    cmd.Parameters.AddWithValue("@Diagnostico", model.Diagnostico);
                    cmd.Parameters.AddWithValue("@Tratamiento", model.Tratamiento);
                    cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrEmpty(model.Observaciones) ? (object)DBNull.Value : model.Observaciones);
                    cmd.ExecuteNonQuery();
                    resultado = 1; 
                }
            }
            return resultado;
        }

        public List<HistorialMedico> ListarHistorialesPorVeterinario(int idVeterinario)
        {
            List<HistorialMedico> lista = new List<HistorialMedico>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarHistorialesMedicos", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdVeterinario", idVeterinario);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new HistorialMedico
                            {
                                IdHistorial = Convert.ToInt32(dr["IdHistorial"]),
                                NombreMascota = dr["NombreMascota"].ToString(),
                                NombreDueno = dr["NombreDueno"].ToString(), 
                                FechaAtencion = Convert.ToDateTime(dr["FechaAtencion"]),
                                Diagnostico = dr["Diagnostico"].ToString(),
                                Tratamiento = dr["Tratamiento"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
