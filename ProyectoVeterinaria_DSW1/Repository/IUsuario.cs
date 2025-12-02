using ProyectoVeterinaria_DSW1.Models;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IUsuario:Icrud<Usuario>
    {
        //agregar metodos especiales si se requieren
        int InsertarUsuario(Usuario objeto);
        Usuario BuscarPorEmail(string email);



    }
}
