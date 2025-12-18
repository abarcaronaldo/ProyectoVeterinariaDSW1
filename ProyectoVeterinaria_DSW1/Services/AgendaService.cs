using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;

namespace ProyectoVeterinaria_DSW1.Services
{
    public class AgendaService
    {
        IAgenda _agenda;
        public AgendaService(IAgenda agenda)
        {
            _agenda = agenda;
        }

        public List<AgendaVeterinario> ListarAgendasVeterinario(int idVeterinario)
        {
            return _agenda.ListarAgendasVeterinario(idVeterinario);
        }
        public AgendaVeterinario ObtenerIdAgenda(int idAgenda)
        {
            return _agenda.ObtenerIdAgenda(idAgenda);
        }
        public int RegistrarAgenda(AgendaVeterinario model)
        {
            return _agenda.RegistrarAgenda(model);
        }

        public int ActualizarAgenda(AgendaVeterinario model)
        {
            return _agenda.ActualizarAgenda(model);
        }

        public int EliminarAgenda(int idAgenda)
        {
            return _agenda.EliminarAgenda(idAgenda);
        }

        public bool TieneCitasPendientes(int idAgenda)
        {
            return _agenda.TieneCitasPendientes(idAgenda);
        }

    }
}
