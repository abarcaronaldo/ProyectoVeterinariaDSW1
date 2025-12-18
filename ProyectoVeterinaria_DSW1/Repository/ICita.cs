using ProyectoVeterinaria_DSW1.Models;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface ICita
    {
        List<Cita> ListarCitasPorVeterinario(int idVeterinario, int? idEstado);
        DetalleCita VerDetalleCita(int idCita);
        int ActualizarEstadoCita(int idCita, int nuevoIdEstado);
        public Cita ObtenerCitaPorId(int idCita);
    }
}
