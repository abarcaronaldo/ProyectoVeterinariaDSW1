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
            string idVetStr = HttpContext.Session.GetString("IdVeterinario");
            if (string.IsNullOrEmpty(idVetStr))
            {
                return RedirectToAction("Login", "Login");
            }

            int idVeterinario = int.Parse(idVetStr);

            var resumen = await Task.Run(() => _veterinario.ObtenerResumen(idVeterinario));

            ViewBag.CitasHoy = resumen.CitasHoy;
            ViewBag.CitasPendientes = resumen.CitasPendientes;
            ViewBag.CitasAtendidas = resumen.CitasAtendidas;
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
