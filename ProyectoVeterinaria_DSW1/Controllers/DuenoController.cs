using Microsoft.AspNetCore.Mvc;
using ProyectoVeterinaria_DSW1.Services;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class DuenoController : Controller
    {
        DuenoService _dueno;
        public DuenoController(DuenoService dueno)
        {
            _dueno = dueno;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Informacion()
        {
            var idUsuarioStr = HttpContext.Session.GetString("IdDueno");
            if (!int.TryParse(idUsuarioStr, out int idDueno))
                return RedirectToAction("Login", "Login");
            return View(await Task.Run(() => _dueno.BuscarDuenoId(idDueno)));
        }


        [HttpGet]
        public IActionResult ReservarCita()
        {
            ViewBag.FechaPorDefecto = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
    }
}
