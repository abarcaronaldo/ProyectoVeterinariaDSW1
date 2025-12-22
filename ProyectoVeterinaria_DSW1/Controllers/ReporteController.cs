using Microsoft.AspNetCore.Mvc;
using ProyectoVeterinaria_DSW1.Services;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class ReporteController : Controller
    {
        CitaService _cita;

        public ReporteController(CitaService cita)
        {
            _cita = cita;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CitasSemana(DateOnly? fecha, int page=1)
        {
            var idStr = HttpContext.Session.GetString("IdVeterinario");
            if (!int.TryParse(idStr, out int idVeterinario))
                return RedirectToAction("Login", "Login");

            DateOnly fechaReferencia = fecha ?? DateOnly.FromDateTime(DateTime.Today);
            int pageSize = 10;

            var resultado = _cita.ObtenerReporteSemana(
                idVeterinario,
                fechaReferencia,
                page,
                pageSize
            );

            ViewBag.FechaSeleccionada = fechaReferencia.ToString("yyyy-MM-dd");
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRegistros = resultado.Total;

            return View(resultado.Items);
        }

        [HttpGet]
        public IActionResult CitasMensual(DateOnly? fechaInicio, DateOnly? fechaFin, int page = 1)
        {
            var idStr = HttpContext.Session.GetString("IdVeterinario");
            if (!int.TryParse(idStr, out int idVeterinario))
                return RedirectToAction("Login", "Login");

            DateOnly inicio = fechaInicio ?? DateOnly.FromDateTime(DateTime.Today);
            DateOnly fin = fechaFin ?? DateOnly.FromDateTime(DateTime.Today);
            int pageSize = 15;

            var resultado = _cita.ObtenerReporteMensual(
                idVeterinario,
                inicio,
                fin,
                page,
                pageSize
            );

            ViewBag.FechaInicio = inicio.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fin.ToString("yyyy-MM-dd");
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRegistros = resultado.Total;

            return View(resultado.Items);
        }
    }
}
