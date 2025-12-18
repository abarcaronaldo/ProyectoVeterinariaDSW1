using Microsoft.AspNetCore.Mvc;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

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

        [HttpGet]
        public async Task<IActionResult> MisCitas()
        {
            return await ObtenerCitasYMostrarVista(null);
        }

        [HttpPost]
        public async Task<IActionResult> MisCitas(int? IdEstadoFiltro)
        {
            return await ObtenerCitasYMostrarVista(IdEstadoFiltro);
        }

        private async Task<IActionResult> ObtenerCitasYMostrarVista(int? IdEstadoFiltro)
        {       
            CargarEstados(IdEstadoFiltro);

            string idVetSession = HttpContext.Session.GetString("IdVeterinario");

            if (string.IsNullOrEmpty(idVetSession))
            {
                return RedirectToAction("Login", "Login");
            }

            int idVeterinarioReal = int.Parse(idVetSession);

            try
            {
                List<Cita> listaCitas = await Task.Run(() =>
                {
                    return _citaService.ListarCitasPorVeterinario(idVeterinarioReal, IdEstadoFiltro);
                });

                ViewBag.Filtro = IdEstadoFiltro.HasValue ? $"Estado ID: {IdEstadoFiltro.Value}" : "TODOS los Estados";
                ViewBag.Veterinario = idVeterinarioReal;

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
                return RedirectToAction("MisCitas"); 
            }

            DetalleCita detalle = await Task.Run(() =>
            {
                return _citaService.VerDetalleCita(idCita);
            });

            if (detalle == null)
            {
                TempData["MensajeError"] = $"No se encontró el detalle para la Cita ID: {idCita}";
                return RedirectToAction("MisCitas");
            }
            return View(detalle); 
        }

        [HttpGet]
        public IActionResult EstadoCita(int id)
        {
            var cita = _citaService.ObtenerCitaPorId(id);
            if (cita == null) return NotFound();

            return View(cita);
        }

        [HttpPost]
        public IActionResult CambiarEstado(int idCita, int nuevoIdEstado)
        {
            int res = _citaService.ActualizarEstadoCita(idCita, nuevoIdEstado);

            if (res > 0)
                TempData["MensajeExito"] = "Estado actualizado correctamente.";
            else
                TempData["MensajeError"] = "No se pudo cambiar el estado.";

            return RedirectToAction("MisCitas");
        }
    }
}
