using Microsoft.Data.SqlClient;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.ViewsModel;
using System.Data;

namespace ProyectoVeterinaria_DSW1.DAO
{
    public class AgendaDAO : IAgenda
    {
        string cadena = (new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).GetConnectionString("cn") ?? "";

        public string actualizar(AgendaVeterinario entidad)
        {
            throw new NotImplementedException();
        }

        public int ActualizarAgenda(AgendaVeterinario model)
        {
            int resultado = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarAgendaVet", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAgenda", model.IdAgenda);
                    cmd.Parameters.AddWithValue("@HoraInicio", model.HoraInicio);
                    cmd.Parameters.AddWithValue("@HoraFin", model.HoraFin);
                    cmd.Parameters.AddWithValue("@CupoMaximoNuevo", model.CupoMaximo);

                    resultado = cmd.ExecuteNonQuery() > 0 ? 1 : 0;
                }
            }
            return resultado;
        }

        public string agregar(AgendaVeterinario entidad)
        {
            throw new NotImplementedException();
        }

        public AgendaVeterinario buscar(int id)
        {
            AgendaVeterinario agenda = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_buscarAgendaPor_Id", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAgenda", id);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            agenda = new AgendaVeterinario
                            {
                                IdAgenda = Convert.ToInt32(dr["IdAgenda"]),
                                IdVeterinario = Convert.ToInt32(dr["IdVeterinario"]),
                                DiaSemana = Convert.ToInt32(dr["DiaSemana"]),
                                HoraInicio = (TimeSpan)dr["HoraInicio"],
                                HoraFin = (TimeSpan)dr["HoraFin"],
                                CupoMaximo = Convert.ToInt32(dr["CupoMaximo"]),
                                CupoDisponible = Convert.ToInt32(dr["CupoDisponible"])
                            };
                        }
                    }
                }
            }

            return agenda;
        }

        public IEnumerable<AgendaDisponibilidadViewModel> BuscarDisponibilidad(DateOnly fecha)
        {
            List<AgendaDisponibilidadViewModel> lista = new List<AgendaDisponibilidadViewModel>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerDiponibilidadPorFechav2", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fecha", fecha);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new AgendaDisponibilidadViewModel
                            {
                                IdAgenda = Convert.ToInt32(dr["IdAgenda"]),
                                IdVeterinario = Convert.ToInt32(dr["IdVeterinario"]),
                                NombreVeterinario = dr["NombreVeterinario"].ToString(),
                                HoraInicio = (TimeSpan)dr["HoraInicio"],
                                HoraFin = (TimeSpan)dr["HoraFin"],
                                CupoMaximo = Convert.ToInt32(dr["CupoMaximo"]),
                                CupoDisponible = Convert.ToInt32(dr["CupoDisponible"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public string eliminar(AgendaVeterinario entidad)
        {
            throw new NotImplementedException();
        }

        public int EliminarAgenda(int idAgenda)
        {
            int resultado = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EliminarAgenda", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAgenda", idAgenda);

                    object scalarResult = cmd.ExecuteScalar();

                    if (scalarResult != null)
                    {
                        resultado = Convert.ToInt32(scalarResult);
                    }
                }
            }
            return resultado;
        }

        public IEnumerable<AgendaVeterinario> listado()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AgendaVeterinario> ListarAgendasVeterinario(int idVeterinario)
        {
            List<AgendaVeterinario> lista = new List<AgendaVeterinario>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarAgendasVeter", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdVeterinario", idVeterinario);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new AgendaVeterinario
                            {
                                IdAgenda = (int)dr["IdAgenda"],
                                DiaSemana = (int)dr["DiaSemana"],
                                HoraInicio = dr.GetTimeSpan(dr.GetOrdinal("HoraInicio")),
                                HoraFin = dr.GetTimeSpan(dr.GetOrdinal("HoraFin")),
                                CupoMaximo = (int)dr["CupoMaximo"],
                                CupoDisponible = (int)dr["CupoDisponible"]
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public TimeSpan ObtenerHoraFin(int idAgenda)
        {
            TimeSpan horaFin = default;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerHoraFin", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAgenda", idAgenda);

                    cn.Open();
                    var result = cmd.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        horaFin = TimeSpan.Parse(result.ToString());
                    }
                }
            }

            return horaFin;


        }

        public IEnumerable<TimeSpan> ObtenerHorasOcupadas(int idAgenda, DateOnly fecha)
        {
            var lista = new List<TimeSpan>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ConsultarHorasOcupadas", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAgenda", idAgenda);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add((TimeSpan)dr["HoraCita"]);
                        }
                    }
                }
            }

            return lista;
        }

        public int RegistrarAgenda(AgendaVeterinario model)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_RegistrarAgendaVet", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdVeterinario", SqlDbType.Int).Value = model.IdVeterinario;
                        cmd.Parameters.Add("@DiaSemana", SqlDbType.Int).Value = model.DiaSemana;
                        cmd.Parameters.Add("@HoraInicio", SqlDbType.Time).Value = model.HoraInicio;
                        cmd.Parameters.Add("@HoraFin", SqlDbType.Time).Value = model.HoraFin;
                        cmd.Parameters.Add("@CupoMaximo", SqlDbType.Int).Value = model.CupoMaximo;
                        int filas = cmd.ExecuteNonQuery();
                        return (filas > 0) ? 1 : 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) return -2;

                System.Diagnostics.Debug.WriteLine("SQL ERROR: " + ex.Message);
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR REAL: " + ex.Message);
                return 0;
            }
        }

        public bool TieneCitasPendientes(int idAgenda)
        {
            int conteo = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ContarCitasPorAgenda", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdAgenda", idAgenda);
                    conteo = (int)cmd.ExecuteScalar();
                }
            }
            return conteo > 0;
        }
    }
}
