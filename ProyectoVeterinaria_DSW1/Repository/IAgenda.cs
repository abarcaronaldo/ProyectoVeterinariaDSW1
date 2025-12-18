using ProyectoVeterinaria_DSW1.Models;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IAgenda
    {
        List<AgendaVeterinario> ListarAgendasVeterinario(int idVeterinario);
        AgendaVeterinario ObtenerIdAgenda(int idAgenda);
        int RegistrarAgenda(AgendaVeterinario model);
        int ActualizarAgenda(AgendaVeterinario model);
        int EliminarAgenda(int idAgenda);
        bool TieneCitasPendientes(int idAgenda);
    }
}
