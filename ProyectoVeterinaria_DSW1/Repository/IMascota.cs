using ProyectoVeterinaria_DSW1.Models;

namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IMascota: ICrud<Mascota>
    {
        //metodos especiales si se requieren
        IEnumerable<Mascota> BuscarMascotasPorDueno(int idDueno);
    }
}
