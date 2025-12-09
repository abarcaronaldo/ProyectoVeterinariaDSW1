using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IAgenda:ICrud<Agenda>
    {
        IEnumerable<AgendaDisponibilidadViewModel> BuscarDisponibilidad(DateOnly fecha);
        IEnumerable<TimeSpan> ObtenerHorasOcupadas(int idAgenda, DateOnly fecha);
        TimeSpan ObtenerHoraFin(int idAgenda);
    }
}
