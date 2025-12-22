using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface ICita:ICrud<Cita>, IConsulta<Cita>
    {
        //metodos adicionales solo para cita
        int RegistrarCita(Cita objeto);
        IEnumerable<CitaListadoViewModel> MisCitas(int idDueno); //del dueño
        CitaListadoViewModel BuscarCita(int idCita, int idDueno);

        //----------------------
        IEnumerable<DetalleCitaViewModel> ListarCitasPorVeterinario(int idVeterinario, int? idEstado, string nombreDueno);
        DetalleCita VerDetalleCita(int idCita);
        int ActualizarEstadoCita(int idCita, int nuevoIdEstado);
        public DetalleCitaViewModel ObtenerCitaPorId(int idCita);

        //----REPORTE--------------
        Paginado<ReporteCitaSemanalVM> ReporteCitaSemana(int idVeterinario, DateOnly fechaReferencia, int page, int pageSize);
        Paginado<ReporteCitaSemanalVM> ReporteCitaMes(int idVeterinario, DateOnly fechaInicio, DateOnly fechaFin, int page, int pageSize);

    }
}
