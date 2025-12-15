using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;

namespace ProyectoVeterinaria_DSW1.Services
{
    public class CitaService
    {
        ICita _citas;
        public CitaService(ICita citas)
        {
            _citas = citas;
        }

        public List<Cita> ListarCitasPorVeterinario(int idVeterinario, int? idEstado)
        {
            return _citas.ListarCitasPorVeterinario(idVeterinario, idEstado);
        }

        public DetalleCita VerDetalleCita(int idCita)
        {
            return _citas.VerDetalleCita(idCita);
        }
    }
}
