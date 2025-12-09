using ProyectoVeterinaria_DSW1.Constants;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using System.Reflection;

namespace ProyectoVeterinaria_DSW1.Services
{
    public class MascotaService
    {
        IMascota _mascota;

        public MascotaService(IMascota mascota)
        {
            _mascota = mascota;
        }

        public string AgregarMascota(Mascota objeto)
        {
            string mensaje = "";
            try
            {
                mensaje = _mascota.agregar(objeto);
                return mensaje;

            }
            catch (Exception ex) { mensaje = ex.Message; }
            return mensaje;
        }

        public IEnumerable<Mascota> ListadoMascotaPorDueno(int idDueno)
        {
            return _mascota.BuscarMascotasPorDueno(idDueno);
        }
    }
}
