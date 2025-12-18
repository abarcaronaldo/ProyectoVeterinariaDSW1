using ProyectoVeterinaria_DSW1.Models;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IUsuario:ICrud<Usuario>, IConsulta<Usuario>
    {
        //agregar metodos especiales si se requieren
        int InsertarUsuario(Usuario objeto);
        Usuario BuscarPorEmail(string email);



    }
}
