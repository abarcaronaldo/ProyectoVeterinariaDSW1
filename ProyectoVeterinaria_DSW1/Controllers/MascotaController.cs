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
    }
}
