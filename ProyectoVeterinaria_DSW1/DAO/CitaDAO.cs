using Microsoft.Data.SqlClient;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using System.Data;

namespace ProyectoVeterinaria_DSW1.DAO
{
    public class CitaDAO : ICita
    {
        string cadena = (new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).GetConnectionString("cn") ?? "";

        public string actualizar(Cita entidad)
        {
            throw new NotImplementedException();
        }

        public string agregar(Cita entidad)
        {
            throw new NotImplementedException();
        }

        public Cita buscar(int id)
        {
            throw new NotImplementedException();
        }

        public string eliminar(Cita entidad)
        {
            throw new NotImplementedException();
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
