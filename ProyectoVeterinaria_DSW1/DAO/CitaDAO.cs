using Microsoft.Data.SqlClient;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using System.Data;

namespace ProyectoVeterinaria_DSW1.DAO
{
    public class CitaDAO : ICita
    {
        string cadena = (new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).GetConnectionString("cn") ?? "";

        public void ActualizarEstado(int idCita, int nuevoIdEstado)
        {
            throw new NotImplementedException();
        }

        public List<Cita> ListarCitasPorVeterinario(int idVeterinario, int? idEstado)
        {
            List<Cita> temporal = new List<Cita>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_ListarCitasVeterinari", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdVeterinario", idVeterinario);

                    if (idEstado.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@IdEstado", idEstado.Value);
                    }
                    else
                    {
                        // Pasamos DBNull para activar el "listar todo" en el SP
                        cmd.Parameters.AddWithValue("@IdEstado", DBNull.Value);
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            temporal.Add(new Cita
                            {
                                idcita = (int)dr["IdCita"],
                                fechacita = (DateTime)dr["FechaCita"],
                                horacita = dr.GetTimeSpan(dr.GetOrdinal("HoraCita")),
                                estado = dr["Estado"].ToString(),
                                especie = dr["Especie"].ToString(),
                                nombredueno = dr["NombreDueno"].ToString(),
                            });
                        }
                    }
                }
            }
            return temporal;
        }

        public DetalleCita VerDetalleCita(int idCita)
        {
            DetalleCita temporal = new DetalleCita();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_VerDetalleCita", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCita", idCita);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            temporal = new DetalleCita
                            {
                                idcita = (int)dr["IdCita"],
                                fechacita = (DateTime)dr["FechaCita"],
                                horacita = dr.GetTimeSpan(dr.GetOrdinal("HoraCita")),
                                motivo = dr["Motivo"].ToString(),
                                fechacreacion = (DateTime)dr["FechaCreacion"],
                                estado = dr["Estado"].ToString(),
                                nombremascota = dr["NombreMascota"].ToString(),  
                                especie = dr["Especie"].ToString(),
                                raza = dr["Raza"].ToString(),
                                nombredueno = dr["NombreDueno"].ToString(),
                                apellidodueno = dr["ApellidoDueno"].ToString(),
                                telefonodueno = dr["TelefonoDueno"].ToString(),
                            };
                        }
                    }
                }
            }
            return temporal;
        }
    }
}
