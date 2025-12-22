using Microsoft.Data.SqlClient;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.ViewsModel;
using System.Data;

namespace ProyectoVeterinaria_DSW1.DAO
{
    public class VeterinarioDAO : IVeterinario
    {
        string cadena = (new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).GetConnectionString("cn") ?? "";

        public string actualizar(Veterinario entidad)
        {
            throw new NotImplementedException();
        }

        public string agregar(Veterinario entidad)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_insertar_veterinario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", entidad.idusuario);
                        cmd.Parameters.AddWithValue("@Nombre", entidad.nombre);
                        cmd.Parameters.AddWithValue("@Apellido", entidad.apellido);
                        cn.Open();
                        int i = cmd.ExecuteNonQuery();
                        mensaje = $"Se ha registrado {i} un veterinario";
                    }
                }
            }
            catch (Exception ex) { mensaje = ex.Message; }
            return mensaje;
        }

        public Veterinario buscar(int id)
        {
            Veterinario obj;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_buscarVeterinario_id", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", id);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            obj = new Veterinario
                            {
                                idveterinario = Convert.ToInt32(dr["IdVeterinario"]),
                                idusuario = Convert.ToInt32(dr["IdUsuario"]),
                                nombre = dr["Nombre"].ToString(),
                                apellido = dr["Apellido"].ToString(),
                            };
                        }
                        else
                        {
                            obj = new Veterinario();
                        }
                    }
                }
            }
            return obj;
        }

        public string eliminar(Veterinario entidad)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Veterinario> listado()
        {
            throw new NotImplementedException();
        }

        public ResumenVeterinarioViewModel ObtenerResumen(int idVeterinario)
        {
            ResumenVeterinarioViewModel resumen = new ResumenVeterinarioViewModel();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerResumenCitasVeterinario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdVeterinario", idVeterinario);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    resumen.CitasHoy = Convert.ToInt32(dr["CitasHoy"]);
                    resumen.CitasPendientes = Convert.ToInt32(dr["CitasPendientes"]);
                    resumen.CitasAtendidas = Convert.ToInt32(dr["CItasAtendidas"]);
                }
            }
            return resumen;
        }

        public Veterinario ObtenerVeterinarioPorId(int idVeterinario)
        {
            Veterinario obj;
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_buscarVeterinario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdVeterinario", idVeterinario);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            obj = new Veterinario
                            {
                                idveterinario = Convert.ToInt32(dr["IdVeterinario"]),
                                idusuario = Convert.ToInt32(dr["IdUsuario"]),
                                nombre = dr["Nombre"].ToString(),
                                apellido = dr["Apellido"].ToString(),
                            };
                        }
                        else
                        {
                            obj = new Veterinario();
                        }
                    }
                }
            }
            return obj;
        }
    }
}
