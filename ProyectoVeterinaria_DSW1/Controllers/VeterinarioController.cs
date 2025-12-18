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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IdVeterinario")))
            {         
                return RedirectToAction("Login", "Login");
            }

            // Aquí podrías llamar a un servicio para traer los datos reales en lugar de fijos
            ViewBag.CitasHoy = 4;
            ViewBag.CitasPendientes = 2;
            ViewBag.CitasAtendidas = 2;

            return View();
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
                return View(model);
            }

            string resultado = await Task.Run(() => _veterinario.AgregarVeterinario(model));

            if (resultado.Contains("Se ha registrado"))
            {
                TempData["MensajeExito"] = "El veterinario y su cuenta de acceso han sido creados.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "No se pudo completar el registro: " + resultado);
            return View(model);
        }
    }
}
