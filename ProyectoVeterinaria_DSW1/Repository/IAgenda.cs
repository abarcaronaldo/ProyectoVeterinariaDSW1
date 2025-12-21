using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IAgenda:ICrud<AgendaVeterinario>, IConsulta<AgendaVeterinario>
    {
        IEnumerable<AgendaDisponibilidadViewModel> BuscarDisponibilidad(DateOnly fecha);
        IEnumerable<TimeSpan> ObtenerHorasOcupadas(int idAgenda, DateOnly fecha);
        TimeSpan ObtenerHoraFin(int idAgenda);
        int EliminarAgenda(int idAgenda);
        bool TieneCitasPendientes(int idAgenda);
        int RegistrarAgenda(AgendaVeterinario model);
        int ActualizarAgenda(AgendaVeterinario model);
        IEnumerable<AgendaVeterinario> ListarAgendasVeterinario(int idVeterinario);
    }
}
