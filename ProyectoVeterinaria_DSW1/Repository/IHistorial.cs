using ProyectoVeterinaria_DSW1.Models;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IHistorial
    {
        IEnumerable<HistorialMedico> ListarHistorialesPorVeterinario(int idVeterinario);
        HistorialMedico ObtenerInfoInicial(int idCita);
        int RegistrarAtencionMedica(HistorialMedico model);
        IEnumerable<HistorialMedico> VerMiHistorialMedico(int idCita);
    }
}
