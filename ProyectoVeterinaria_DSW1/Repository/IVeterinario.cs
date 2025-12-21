using ProyectoVeterinaria_DSW1.Models;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IVeterinario:ICrud<Veterinario>, IConsulta<Veterinario>
    {
        //agregar metodo especiales si se requieren
        Veterinario ObtenerVeterinarioPorId(int idVeterinario);
    }
}
