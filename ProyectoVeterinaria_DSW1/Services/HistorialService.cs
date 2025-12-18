using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;

namespace ProyectoVeterinaria_DSW1.Services
{
    public class HistorialService
    {
       IHistorial _historialRepository;

        public HistorialService(IHistorial historialRepository)
        {
            _historialRepository = historialRepository;
        }

        public HistorialMedico ObtenerInfoInicial(int idCita)
        {
            return _historialRepository.ObtenerInfoInicial(idCita);
        }

        public int RegistrarAtencionMedica(HistorialMedico model)
        {
            return _historialRepository.RegistrarAtencionMedica(model);
        }

        public List<HistorialMedico> ListarHistorialesPorVeterinario(int idVeterinario)
        {
            return _historialRepository.ListarHistorialesPorVeterinario(idVeterinario);
        }
    }
}
