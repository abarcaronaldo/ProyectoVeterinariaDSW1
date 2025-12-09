using Microsoft.AspNetCore.Mvc;
using ProyectoVeterinaria_DSW1.Constants;
using ProyectoVeterinaria_DSW1.Services;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class CitaController : Controller
    {
        CitaService _cita;
        MascotaService _mascota;

        public CitaController(CitaService cita, MascotaService mascota)
        {
            _cita = cita;
            _mascota = mascota;
        }

        //primer paso, seleccionar un horario de las agendas, esto nos muestra una vista de los veterinarios disponibles a esa fecha y su horario.
        [HttpGet]
        public async Task<IActionResult> BuscarDisponibilidad(DateOnly fecha)
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);


            if (fecha < hoy)
            {
                return View(new List<AgendaDisponibilidadViewModel>());
            }

            ViewBag.FechaSeleccionada = fecha.ToString("yyyy-MM-dd");
            return View(await Task.Run(()=> _cita.ConsultarDisponibilidad(fecha)));
        }

        //segundo paso, ingresar una hora especifica para la cita, esto nos muestra una vista para ingresar un hora, que luego el sistema ajusta.
        [HttpGet]
        public async Task<IActionResult> SeleccionarHora(int idAgenda, DateOnly fecha)
        {
            //buscamos la agenda
            var agenda = _cita.ObtenerAgendaPorId(idAgenda);

            if (agenda == null)
            {
                TempData["Error"] = "La agenda seleccionada ya no existe.";
                return RedirectToAction("ReservarCita");
            }

            var vm = new SeleccionarHoraViewModel
            {
                IdAgenda = agenda.IdAgenda,
                FechaCita = fecha,
                HoraInicio = agenda.HoraInicio,
                HoraFin = agenda.HoraFin,
                DuracionCitaMin = HoraCita.DURACION
            };

            return View(await Task.Run(()=>vm));
        }

        //tercer paso, procesamos los datos, esto no es una vista - solo procesa todo y redirecciona hacia la vista ConfimarCita.
        
        [HttpPost]
        public IActionResult SeleccionarHora(SeleccionarHoraViewModel vm)
        {
            var horaSugerida = _cita.ObtenerHoraSugerida(vm.IdAgenda, vm.FechaCita, vm.HoraDeseada!, vm.DuracionCitaMin);

            if (horaSugerida == null)
            {
                TempData["Error"] = "No hay horarios disponibles para esta agenda.";
                return RedirectToAction("SeleccionarHora", new { idAgenda = vm.IdAgenda, fecha = vm.FechaCita });
            }

            int idDueno = int.Parse(HttpContext.Session.GetString("IdDueno"));

            TempData["HoraSugerida"] = horaSugerida.Value.ToString(@"hh\:mm");
            TempData["IdAgenda"] = vm.IdAgenda;
            TempData["FechaCita"] = vm.FechaCita.ToString("yyyy-MM-dd");
            TempData["IdDueno"] = idDueno;

            return RedirectToAction("ConfirmarCita");
        }

        //cuarto paso, vista para confirmar la cita, selecionar mascota y motivo de cita
        [HttpGet]
        public IActionResult ConfirmarCita()
        {
            if (TempData["HoraSugerida"] == null ||
                TempData["IdAgenda"] == null ||
                TempData["FechaCita"] == null)
            {
                return RedirectToAction("ReservarCita", "Dueno");
            }

            int idDueno = int.Parse(HttpContext.Session.GetString("IdDueno"));

            // Obtener mascotas del dueño
            var mascotas = _mascota.ListadoMascotaPorDueno(idDueno);

            // Pasar datos a la vista mediante ViewBag
            ViewBag.Mascotas = mascotas;
            ViewBag.HoraSugerida = TempData["HoraSugerida"].ToString();
            ViewBag.IdAgenda = Convert.ToInt32(TempData["IdAgenda"]);
            ViewBag.FechaCita = TempData["FechaCita"].ToString();

            return View();
        }

        //quinto paso, procesar todo y insertar la reserva de la cita
        [HttpPost]
        public IActionResult ConfirmarCita(RegistrarCitaViewModel vm)
        {
            int idDueno = int.Parse(HttpContext.Session.GetString("IdDueno"));
            var idCita = _cita.RegistrarCita(vm, idDueno);

            //pasamos datos de confirmacion a mostrar
            TempData["FechaCita"] = vm.FechaCita.ToString("yyyy-MM-dd");
            TempData["HoraCita"] = vm.HoraCita.ToString(@"hh\:mm");
            TempData["Motivo"] = vm.Motivo;


            if (idCita <= 0)
            {
                TempData["Error"] = "No se pudo registrar la cita. El horario ya no está disponible.";
                TempData["Exito"] = false;
            }
            else
            {
                TempData["Exito"] = true;
            }

            return RedirectToAction("CitaRegistrada");
        }


        //sexto paso, una vez procesado y todo correcto o si hay error, vista final de confirmacion
        [HttpGet]
        public IActionResult CitaRegistrada()
        {
            //si no hay datos mandamos a reservar
            if (TempData["FechaCita"] == null || TempData["HoraCita"] == null)
            {
                return RedirectToAction("ReservarCita", "Dueno");
            }

            //convertimos a string para mostrar
            var fechaStr = TempData["FechaCita"]!.ToString()!;
            var horaStr = TempData["HoraCita"]!.ToString()!;

            DateOnly.TryParse(fechaStr, out var fechaCita);
            TimeSpan.TryParse(horaStr, out var horaCita);

            //pasamos a la vista
            ViewBag.FechaCita = fechaCita;
            ViewBag.HoraCita = horaCita;
            ViewBag.Motivo = TempData["Motivo"]?.ToString();
            ViewBag.Exito = TempData["Exito"] as bool? ?? false;
            ViewBag.Error = TempData["Error"]?.ToString();

            return View();
        }


    }
}
