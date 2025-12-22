using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IVeterinario:ICrud<Veterinario>, IConsulta<Veterinario>
    {
        //agregar metodo especiales si se requieren
        Veterinario ObtenerVeterinarioPorId(int idVeterinario);
        ResumenVeterinarioViewModel ObtenerResumen(int idVeterinario);
    }
}
