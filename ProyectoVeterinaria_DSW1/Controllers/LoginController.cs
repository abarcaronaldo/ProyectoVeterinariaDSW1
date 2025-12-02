using Microsoft.AspNetCore.Mvc;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Services;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Controllers
{
    public class LoginController : Controller
    {
        DuenoService _dueno;
        UsuarioService _usuario;

        public LoginController(DuenoService dueno, UsuarioService usuario)
        {
            _dueno = dueno;
            _usuario = usuario;
        }

        //front login principal
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
            HttpContext.Session.SetString("UsuarioId",
                                  usuario.idusuario.ToString());

            HttpContext.Session.SetString("IdRol",
                                          usuario.idrol.ToString());

            if (usuario.idrol == 1)
            {
                return RedirectToAction("Index", "Dueno");
            }
            else if (usuario.idrol == 2)
            {
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
