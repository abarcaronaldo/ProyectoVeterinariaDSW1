using Microsoft.AspNetCore.Mvc;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.Services;
using System.Reflection;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class MascotaController : Controller
    {
        MascotaService _mascota;
        public MascotaController(MascotaService mascota)
        {
            _mascota = mascota;
        }

        public async Task<IActionResult> ListadoMascota()
        {
            //id del dueno
            var idStr = HttpContext.Session.GetString("IdDueno");
            if (string.IsNullOrEmpty(idStr))
            {
                //sesion expirada
                return RedirectToAction("Login", "Login");
            }

            int idDueno = int.Parse(idStr);
            var lista = await Task.Run(() => _mascota.ListadoMascotaPorDueno(idDueno));
            if (lista == null || !lista.Any())
            {
                TempData["Mensaje"] = "No tiene mascotas a cargo.";
                return View(new List<Mascota>());
            }

            return View(lista);

        }


        public async Task<IActionResult> AgregarMascota()
        {
            return View(await Task.Run(() => new Mascota()));
        }

        [HttpPost]
        public async Task<IActionResult> AgregarMascota(Mascota objeto)
        {
            if (!ModelState.IsValid)
            {
                return View(await Task.Run(() => objeto));
            }

            string? idDueno = HttpContext.Session.GetString("IdDueno");
            if (string.IsNullOrEmpty(idDueno))
            {
                ModelState.AddModelError("", "No se encontró el dueño en la sesión.");
                return View(await Task.Run(() => objeto));
            }

            objeto.iddueno = int.Parse(idDueno);
            ModelState.AddModelError("", _mascota.AgregarMascota(objeto));
            return View(await Task.Run(() => objeto));
        }

        public async Task<IActionResult> EditarMascota(int id)
        {
            return View(await Task.Run(() => _mascota.BuscarMascota(id)));
        }

        [HttpPost]
        public async Task<IActionResult> EditarMascota(Mascota objeto)
        {
            //id dueno
            var idStr = HttpContext.Session.GetString("IdDueno");
            if (!int.TryParse(idStr, out int idDueno))
            {
                return RedirectToAction("Login", "Login");
            }

            objeto.iddueno = idDueno;
            string mensaje = _mascota.ActualizarMascota(objeto);

            //si paso algo
            if (!mensaje.Contains("correctamente"))
            {
                ModelState.AddModelError("", mensaje);
                return View(await Task.Run(() => objeto));
            }

            //si fue bien
            TempData["Mensaje"] = mensaje;
            return RedirectToAction("ListadoMascota");

        }

        [HttpGet]
        public async Task<IActionResult> EliminarMascota(int id)
        {
            return View(await Task.Run(() => _mascota.BuscarMascota(id)));
        }

        [HttpPost]
        public IActionResult EliminarMascotaConfirmado(int id)
        {
            var idStr = HttpContext.Session.GetString("IdDueno");
            if (!int.TryParse(idStr, out int idDueno))
                return RedirectToAction("Login", "Login");

            TempData["Mensaje"] = _mascota.EliminarMascota(id, idDueno);
            return RedirectToAction("ListadoMascota");
        }


    }
}
