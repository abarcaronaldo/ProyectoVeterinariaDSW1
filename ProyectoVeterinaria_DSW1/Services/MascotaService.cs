using NuGet.Protocol.Core.Types;
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

        public string ActualizarMascota(Mascota objeto)
        {
            string mensaje = "";
            try
            {
                mensaje = _mascota.actualizar(objeto);
                return mensaje;

            }
            catch { mensaje = "Ha ocurrido un problema al actualizar la mascota"; }
            return mensaje;
        }

        public IEnumerable<Mascota> ListadoMascotaPorDueno(int idDueno)
        {
            return _mascota.BuscarMascotasPorDueno(idDueno);
        }

        public Mascota BuscarMascota(int idMacota)
        {
            return _mascota.buscar(idMacota);
        }

        public string EliminarMascota(int idMascota, int idDueno)
        {
            //entidad mascota
            Mascota mascota = new Mascota
            {
                idmascota = idMascota,
                iddueno = idDueno
            };

            return _mascota.eliminar(mascota);
        }

    }
}
