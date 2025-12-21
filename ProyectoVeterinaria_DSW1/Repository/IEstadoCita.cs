using ProyectoVeterinaria_DSW1.Models;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IEstadoCita
    {
        IEnumerable<EstadoCita> ListarEstados();
    }
}
