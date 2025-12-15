using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;

namespace ProyectoVeterinaria_DSW1.Services
{
    public class EstadoCitaService
    {
        IEstadoCita _estadoCita;

        public EstadoCitaService(IEstadoCita estadoCita)
        {
            _estadoCita = estadoCita;
        }

        // Método que devuelve la lista de estados en el formato SelectListItem
        public List<SelectListItem> ObtenerEstadosParaFiltro(int? estadoSeleccionadoId)
        {
            List<EstadoCita> estadosDb = _estadoCita.ListarEstados();

            var listaFiltro = estadosDb.Select(e => new SelectListItem
            {
                Value = e.IdEstado.ToString(),
                Text = e.Estado,  
                Selected = estadoSeleccionadoId.HasValue && e.IdEstado == estadoSeleccionadoId.Value
            }).ToList();

            listaFiltro.Insert(0, new SelectListItem
            {
                Value = null, 
                Text = "-- Todos los Estados --",
                Selected = !estadoSeleccionadoId.HasValue 
            });

            return listaFiltro;
        }
    }
}
