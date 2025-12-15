using ProyectoVeterinaria_DSW1.Models;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface ICita
    {
        List<Cita> ListarCitasPorVeterinario(int idVeterinario, int? idEstado);
        DetalleCita VerDetalleCita(int idCita);
        void ActualizarEstado(int idCita, int nuevoIdEstado);
    }
}
