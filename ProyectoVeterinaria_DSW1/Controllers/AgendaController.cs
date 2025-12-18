using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.Services;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class AgendaController : Controller
    {
        AgendaService _agendaService;

        public AgendaController(AgendaService agendaService)
        {
            _agendaService = agendaService;
        }


        [HttpGet]
        public IActionResult ListarAgendas()
        {
            string idVetStr = HttpContext.Session.GetString("IdVeterinario");
            if (string.IsNullOrEmpty(idVetStr)) return RedirectToAction("Login", "Login");

            int idVeterinario = int.Parse(idVetStr);

            var horarios = _agendaService.ListarAgendasVeterinario(idVeterinario);
            return View(horarios);
        }

        [HttpGet]
        public IActionResult Crear()
        {

            string idVetStr = HttpContext.Session.GetString("IdVeterinario");
            if (string.IsNullOrEmpty(idVetStr)) return RedirectToAction("Login", "Login");

            int idVeterinarioReal = int.Parse(idVetStr);

            ViewBag.DiasSemana = ObtenerListaDias();

            AgendaVeterinario model = new AgendaVeterinario { IdVeterinario = idVeterinarioReal };

            return View(model);
        }

        [HttpPost]
        public IActionResult Crear(AgendaVeterinario model)
        {
            string idVetStr = HttpContext.Session.GetString("IdVeterinario");
            if (string.IsNullOrEmpty(idVetStr)) return RedirectToAction("Login", "Login");

            model.IdVeterinario = int.Parse(idVetStr);

            if (!ModelState.IsValid)
            {
                ViewBag.DiasSemana = ObtenerListaDias();
                return View(model);
            }

            int resultado = _agendaService.RegistrarAgenda(model);

            if (resultado == 1)
            {
                TempData["MensajeExito"] = "Horario registrado correctamente.";
                return RedirectToAction("ListarAgendas");
            }
            else if (resultado == -2)
            {
                ModelState.AddModelError("", "Ya existe una agenda para este veterinario en el día seleccionado.");
            }
            else
            {
                ModelState.AddModelError("", "No se pudo registrar. Verifique que el día no esté ocupado y que Hora Inicio < Hora Fin.");
            }
            ViewBag.DiasSemana = ObtenerListaDias();
            return View(model);

        }

        private List<SelectListItem> ObtenerListaDias()
        {
            return new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Lunes" },
            new SelectListItem { Value = "2", Text = "Martes" },
            new SelectListItem { Value = "3", Text = "Miércoles" },
            new SelectListItem { Value = "4", Text = "Jueves" },
            new SelectListItem { Value = "5", Text = "Viernes" },
            new SelectListItem { Value = "6", Text = "Sábado" }
        };
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var model = _agendaService.ObtenerIdAgenda(id);
            if (model == null) return NotFound();

            ViewBag.DiasSemana = ObtenerListaDias();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(AgendaVeterinario model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DiasSemana = ObtenerListaDias();
                return View(model);
            }

            if (_agendaService.TieneCitasPendientes(model.IdAgenda))
            {
                ModelState.AddModelError("", "Acción bloqueada: Existen citas vinculadas a este horario. No se puede modificar.");
                ViewBag.DiasSemana = ObtenerListaDias();
                return View(model);
            }

            int resultado = _agendaService.ActualizarAgenda(model);

            if (resultado > 0)
            {
                TempData["MensajeExito"] = "Horario actualizado correctamente.";
                return RedirectToAction("ListarAgendas");
            }

            ModelState.AddModelError("", "No se pudo actualizar. Verifique si el horario entra en conflicto con otra agenda.");
            ViewBag.DiasSemana = ObtenerListaDias();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (_agendaService.TieneCitasPendientes(id))
            {
                TempData["MensajeError"] = "No se puede eliminar la agenda porque existen citas programadas.";
                return RedirectToAction("ListarAgendas");
            }

            int resultado = _agendaService.EliminarAgenda(id);

            if (resultado > 0)
            {
                TempData["MensajeExito"] = "La agenda se ha eliminado correctamente.";
            }
            else
            {
                TempData["MensajeError"] = "Ocurrió un error inesperado al intentar eliminar la agenda.";
            }
            return RedirectToAction("ListarAgendas");

        }

    }
}
