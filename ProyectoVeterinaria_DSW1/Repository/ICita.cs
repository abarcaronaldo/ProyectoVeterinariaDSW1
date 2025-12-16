using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface ICita:ICrud<Cita>, IConsulta<Cita>
    {
        //metodos adicionales solo para cita
        int RegistrarCita(Cita objeto);
        IEnumerable<CitaListadoViewModel> MisCitas(int idDueno);
        CitaListadoViewModel BuscarCita(int idCita, int idDueno);

    }
}
