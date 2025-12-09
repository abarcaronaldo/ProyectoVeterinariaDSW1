using Microsoft.Data.SqlClient;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using System.Data;

namespace ProyectoVeterinaria_DSW1.DAO
{
    public class MascotaDAO : IMascota
    {
        string cadena = (new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).GetConnectionString("cn") ?? "";

        public string actualizar(Mascota entidad)
        {
            throw new NotImplementedException();
        }

        public string agregar(Mascota entidad)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_insertar_mascota", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdDueno", entidad.iddueno);
                        cmd.Parameters.AddWithValue("@Nombre", entidad.nombre);
                        cmd.Parameters.AddWithValue("@Especie", entidad.especie);
                        cmd.Parameters.AddWithValue("@Raza", entidad.raza);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", entidad.fechanacimiento);
                        cmd.Parameters.AddWithValue("@Peso", entidad.peso);
                        cn.Open();
                        int i = cmd.ExecuteNonQuery();
                        mensaje = $"Se ha registrado {i} una nueva mascota";
                    }
                }
            }
            catch (Exception ex) { mensaje = ex.Message; }
            return mensaje;
        }

        public Mascota buscar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Mascota> BuscarMascotasPorDueno(int idDueno)
        {
            var lista = new List<Mascota>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListaMascotar_PorDueno", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDueno", idDueno);
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var mascota = new Mascota
                            {
                                idmascota = (int)dr["IdMascota"],
                                iddueno = (int)dr["IdDueno"],
                                nombre = dr["Nombre"].ToString()!,
                                especie = dr["Especie"].ToString()!,
                                raza = dr["Raza"].ToString()!,
                                fechanacimiento = DateOnly.FromDateTime((DateTime)dr["FechaNacimiento"]
                    ),
                                peso = (decimal)dr["Peso"]
                            };

                            lista.Add(mascota);
                        }
                    }
                }
            }

            return lista;
        }

        public string eliminar(Mascota entidad)
        {
            throw new NotImplementedException();
        }
    }
}
