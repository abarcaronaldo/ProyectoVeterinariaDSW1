using Microsoft.Data.SqlClient;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.ViewsModel;
using System.Data;

namespace ProyectoVeterinaria_DSW1.DAO
{
    public class CitaDAO : ICita
    {
        string cadena = (new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).GetConnectionString("cn") ?? "";

        public string actualizar(Cita entidad)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarCita", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCita", entidad.IdCita);
                    cmd.Parameters.AddWithValue("@IdMascota", entidad.IdMascota);
                    cmd.Parameters.AddWithValue("@Motivo", entidad.Motivo);
                    cmd.Parameters.AddWithValue("@IdDueno", entidad.IdDueno);

                    cn.Open();
                    int resultado = Convert.ToInt32(cmd.ExecuteScalar());

                    if (resultado == 0)
                        return "La cita no existe o no le pertenece";

                    return "Cita actualizada correctamente";
                }
            }
            catch
            {
                return "Ocurrió un error al actualizar la cita";
            }
        }

        public string agregar(Cita entidad)
        {
            throw new NotImplementedException();
        }

        public Cita buscar(int id)
        {
            throw new NotImplementedException();
        }

        public CitaListadoViewModel? BuscarCita(int idCita, int idDueno)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("sp_ObtenerCitaPorId", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCita", idCita);
                cmd.Parameters.AddWithValue("@IdDueno", idDueno);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.Read())
                        return null;

                    return new CitaListadoViewModel
                    {
                        IdCita = dr.GetInt32(dr.GetOrdinal("IdCita")),
                        FechaCita = DateOnly.FromDateTime(dr.GetDateTime(dr.GetOrdinal("FechaCita"))),
                        HoraCita = dr.GetTimeSpan(dr.GetOrdinal("HoraCita")),
                        Motivo = dr["Motivo"]?.ToString(),
                        FechaCreacion = DateOnly.FromDateTime(dr.GetDateTime(dr.GetOrdinal("FechaCreacion"))),
                        IdMascota = dr.GetInt32(dr.GetOrdinal("IdMascota")),
                        NombreMascota = dr["NombreMascota"]?.ToString(),
                        IdEstado = dr.GetInt32(dr.GetOrdinal("IdEstado")),
                        EstadoCita = dr["EstadoCita"]?.ToString(),
                        IdAgenda = dr.GetInt32(dr.GetOrdinal("IdAgenda"))
                    };
                }
            }
        }


        public string eliminar(Cita entidad)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cita> listado()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CitaListadoViewModel> MisCitas(int idDueno)
        {
            var lista = new List<CitaListadoViewModel>();

            using (SqlConnection cn = new SqlConnection(cadena))
            using (SqlCommand cmd = new SqlCommand("sp_ListarCitasPorDueno", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDueno", idDueno);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new CitaListadoViewModel
                        {
                            IdCita = dr.GetInt32(dr.GetOrdinal("IdCita")),
                            FechaCita = DateOnly.FromDateTime(dr.GetDateTime(dr.GetOrdinal("FechaCita"))),
                            HoraCita = dr.GetTimeSpan(dr.GetOrdinal("HoraCita")),
                            Motivo = dr["Motivo"]?.ToString(),
                            FechaCreacion = DateOnly.FromDateTime(dr.GetDateTime(dr.GetOrdinal("FechaCreacion"))),
                            IdMascota = dr.GetInt32(dr.GetOrdinal("IdMascota")),
                            NombreMascota = dr["NombreMascota"].ToString(),
                            IdEstado = dr.GetInt32(dr.GetOrdinal("IdEstado")),
                            EstadoCita = dr["EstadoCita"].ToString(),
                            IdAgenda = dr.GetInt32(dr.GetOrdinal("IdAgenda"))
                        });
                    }
                }
            }
            return lista;
        }

        public int RegistrarCita(Cita objeto)
        {
            int idGenerado = 0;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                //iniciar transaccion
                using (SqlTransaction tran = cn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_RegistrarCita", cn, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@IdAgenda", objeto.IdAgenda);
                            cmd.Parameters.AddWithValue("@IdDueno", objeto.IdDueno);
                            cmd.Parameters.AddWithValue("@IdMascota", objeto.IdMascota);
                            cmd.Parameters.AddWithValue("@FechaCita", objeto.FechaCita);
                            cmd.Parameters.AddWithValue("@HoraCita", objeto.HoraCita);
                            cmd.Parameters.AddWithValue("@Motivo", objeto.Motivo);

                            object result = cmd.ExecuteScalar();
                            idGenerado = Convert.ToInt32(result);
                        }

                        //si todo sale bien
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        //si algo sale mal
                        tran.Rollback();
                        idGenerado = -1;
                    }
                }
            }

            return idGenerado;
        }

    }
}
