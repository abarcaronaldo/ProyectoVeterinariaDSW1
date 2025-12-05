using Microsoft.AspNetCore.Mvc;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.Services;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class VeterinarioController : Controller
    {
        VeterinarioService _veterinario;

        public VeterinarioController(VeterinarioService veterinario)
        {
            _veterinario = veterinario;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UsuarioId") == null)
                return RedirectToAction("Login", "Account");

            //datos de pruba por el momento
            ViewBag.CitasHoy = 4;
            ViewBag.CitasPendientes = 2;
            ViewBag.CitasAtendidas = 2;

            return await Task.Run(()=> View());
        }

        public async Task<IActionResult> AgregarVeterinario()
        {
            return View(await Task.Run(() => new VeterinarioViewModel()));
        }

        [HttpPost]
        public async Task<IActionResult> AgregarVeterinario(VeterinarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await Task.Run(() => model));
            }

            ModelState.AddModelError("", _veterinario.AgregarVeterinario(model));
            return View(await Task.Run(() => model));
        }
    }
}
