using ProyectoVeterinaria_DSW1.Models;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface ICita:ICrud<Cita>
    {
        //metodos adicionales solo para cita
        int RegistrarCita(Cita objeto);
    }
}
