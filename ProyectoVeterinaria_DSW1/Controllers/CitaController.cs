using Microsoft.AspNetCore.Mvc;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Services;
using System.Collections.Generic;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class CitaController : Controller
    {
        CitaService _citaService;
        EstadoCitaService _estadoCitaService;
        public CitaController(CitaService citaService, EstadoCitaService estadoCitaService)
        {
            _citaService = citaService;
            _estadoCitaService = estadoCitaService;
        }

        private void CargarEstados(int? estadoSeleccionado = null)
        {
            ViewBag.ListaEstados = _estadoCitaService.ObtenerEstadosParaFiltro(estadoSeleccionado);
        }
        public async Task<IActionResult> ListarCitas()
        {
            return RedirectToAction("PruebaCita");
        }

        [HttpGet]
        public async Task<IActionResult> PruebaCita()
        {
            int? idEstadoAFiltrar = null;
            // Llama a la lógica de POST para hacer todo el trabajo de filtrado
            return await ObtenerCitasYMostrarVista(idEstadoAFiltrar);
        }

        [HttpPost]
        public async Task<IActionResult> PruebaCita(int? IdEstadoFiltro)
        {
            return await ObtenerCitasYMostrarVista(IdEstadoFiltro);
        }

        private async Task<IActionResult> ObtenerCitasYMostrarVista(int? IdEstadoFiltro)
        {       
            CargarEstados(IdEstadoFiltro);

            // ID de PRUEBA 
            int idVeterinarioPrueba = 1;
            try
            {
                List<Cita> listaCitas = await Task.Run(() =>
                {
                    return _citaService.ListarCitasPorVeterinario(idVeterinarioPrueba, IdEstadoFiltro);
                });

                ViewBag.Filtro = IdEstadoFiltro.HasValue ? $"Estado ID: {IdEstadoFiltro.Value}" : "TODOS los Estados";
                ViewBag.Veterinario = idVeterinarioPrueba;

                return View("PruebaCita", listaCitas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error en el flujo: {ex.Message}";
                return View("PruebaCita", new List<Cita>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> VerDetalle(int idCita)
        {
            if (idCita <= 0)
            {
                return RedirectToAction("ListarCitas"); 
            }

            DetalleCita detalle = await Task.Run(() =>
            {
                return _citaService.VerDetalleCita(idCita);
            });

            if (detalle == null)
            {
                TempData["MensajeError"] = $"No se encontró el detalle para la Cita ID: {idCita}";
                return RedirectToAction("ListarCitas");
            }
            return View(detalle); 
        }
    }
}
