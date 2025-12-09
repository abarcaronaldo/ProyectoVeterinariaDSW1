using Microsoft.AspNetCore.Mvc;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class DuenoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ReservarCita()
        {
            ViewBag.FechaPorDefecto = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

    }
}
