using ProyectoVeterinaria_DSW1.DAO;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;

namespace ProyectoVeterinaria_DSW1.Services
{
    public class UsuarioService
    {
        IUsuario _usuario;
        public UsuarioService(IUsuario usuario)
        {
            _usuario = usuario;
        }

        public Usuario Login(string email, string password)
        {
            //buscar
            Usuario usuario = _usuario.BuscarPorEmail(email);

            //ver si existe
            if (usuario == null)
            {
                return null;
            }
            //verificar passwrod
            if (usuario.password != password)
            {
                return null;
            }

            return usuario;
        }
    }
}
