using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ProyectoVeterinaria_DSW1.DAO
{
    public class UsuarioDAO : IUsuario
    {
        string cadena = (new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()).GetConnectionString("cn") ?? "";

        public string actualizar(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        public string agregar(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        public Usuario buscar(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario BuscarPorEmail(string email)
        {
            Usuario usuario = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_usuario_buscar_por_email", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario = new Usuario
                    {
                        idusuario = Convert.ToInt32(dr["IdUsuario"]),
                        email = dr["Email"].ToString(),
                        password = dr["PasswordHash"].ToString(),
                        idrol = Convert.ToInt32(dr["IdRol"]),
                    };
                }
            }

            return usuario;
        }

        public string eliminar(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        public int InsertarUsuario(Usuario objeto)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_insertar_usuario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", objeto.email);
                        cmd.Parameters.AddWithValue("@password", objeto.password);
                        cmd.Parameters.AddWithValue("@idrol", objeto.idrol);

                        cn.Open();
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<Usuario> listado()
        {
            throw new NotImplementedException();
        }
    }
}
