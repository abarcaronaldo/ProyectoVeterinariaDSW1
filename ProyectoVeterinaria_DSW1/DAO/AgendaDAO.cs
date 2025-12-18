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

        public string actualizar(Agenda entidad)
        {
            throw new NotImplementedException();
        }

        public string agregar(Agenda entidad)
        {
            throw new NotImplementedException();
        }

        public Agenda buscar(int id)
        {
            Agenda agenda =null;

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
                            agenda = new Agenda
                            {
                                IdAgenda = Convert.ToInt32(dr["IdAgenda"]),
                                IdVeterinario = Convert.ToInt32(dr["IdVeterinario"]),
                                DiaSemana = Convert.ToInt32(dr["DiaSemana"]),
                                HoraInicio = (TimeSpan)dr["HoraInicio"],
                                HoraFin = (TimeSpan)dr["HoraFin"],
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

        public string eliminar(Agenda entidad)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Agenda> listado()
        {
            throw new NotImplementedException();
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
    }
    
}
