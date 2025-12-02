using Microsoft.AspNetCore.Mvc;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class VeterinarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
