using Microsoft.AspNetCore.Mvc;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Services;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class HistorialController : Controller
    {
        HistorialService _historialService;

        public HistorialController(HistorialService historialService)
        {
            _historialService = historialService;
        }

        [HttpGet]
        public IActionResult CrearHistorial(int idCita)
        {
            
            HistorialMedico model = _historialService.ObtenerInfoInicial(idCita);

            if (model == null)
            {
                TempData["MensajeError"] = "Cita o Mascota no encontradas.";
                return RedirectToAction("MisCitas", "Cita");
            }
            
            model.IdCita = idCita;
            return View(model); 
        }

        [HttpPost]
        public IActionResult CrearHistorial(HistorialMedico model)
        {
 
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int resultado = _historialService.RegistrarAtencionMedica(model);

            if (resultado > 0)
            {
                TempData["MensajeExito"] = "Registro y atencion médica registrada con éxito.";
                return RedirectToAction("MisCitas", "Cita");
            }
            else
            {
                ModelState.AddModelError("", "Error al registrar la atención médica. Intente nuevamente.");
                return View(model);
            }
            
        }

        public IActionResult ListarHistorial()
        {
            string idVetStr = HttpContext.Session.GetString("IdVeterinario");

            if (string.IsNullOrEmpty(idVetStr))
            {
                return RedirectToAction("Login", "Login");
            }

            int idVeterinario = int.Parse(idVetStr);

            var modelo = _historialService.ListarHistorialesPorVeterinario(idVeterinario);

            return View(modelo);
        }
    }
}
