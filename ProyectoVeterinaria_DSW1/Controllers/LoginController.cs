using Microsoft.AspNetCore.Mvc;
using ProyectoVeterinaria_DSW1.Constants;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.Services;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class LoginController : Controller
    {
        DuenoService _dueno;
        VeterinarioService _veterinario;
        UsuarioService _usuario;

        public LoginController(DuenoService dueno, VeterinarioService veterinario, UsuarioService usuario)
        {
            _dueno = dueno;
            _veterinario = veterinario;
            _usuario = usuario;
        }

        //front login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //post login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            Usuario usuario = _usuario.Login(email, password);

            if (usuario == null)
            {
                ViewBag.Error = "Credenciales incorrectas";
                return View();
            }

            //guardar datos en sesion
            //HttpContext.Session.SetString("UsuarioId",
             //                     usuario.idusuario.ToString());

            if (usuario.idrol == Roles.DUENO)
            {
                Dueno dueno = _dueno.BuscarDuenoId(usuario.idusuario);
                HttpContext.Session.SetString("IdDueno", dueno.idueno.ToString());
                HttpContext.Session.SetString("NombreUsuario", $"{dueno.nombre} {dueno.apellido}");

                return RedirectToAction("Index", "Dueno");
            }
            else if (usuario.idrol == Roles.VETERINARIO)
            {
                Veterinario veterinario = _veterinario.BuscarVeterinarioId(usuario.idusuario);
                HttpContext.Session.SetString("IdVeterinario", veterinario.idveterinario.ToString());
                HttpContext.Session.SetString("NombreUsuario",$"{veterinario.nombre} {veterinario.apellido}");

                return RedirectToAction("Index", "Veterinario");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login"); //mandar de nuevo al inicio
        }


        public async Task<IActionResult> CrearCuenta()
        {
            return View(await Task.Run(() => new DuenoViewModel()));
        }

        [HttpPost]
        public async Task<IActionResult> CrearCuenta(DuenoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await Task.Run(() => model));
            }

            ModelState.AddModelError("", _dueno.RegistrarDueno(model));
            return View(await Task.Run(() => model));
        }
    }
}
