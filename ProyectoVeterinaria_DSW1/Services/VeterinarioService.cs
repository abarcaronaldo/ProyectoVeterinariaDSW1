using ProyectoVeterinaria_DSW1.Constants;
using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Services
{
    public class VeterinarioService
    {
        IUsuario _usuario;
        IVeterinario _veterinario;
        public VeterinarioService(IVeterinario veterinario, IUsuario usuario)
        {
            _veterinario = veterinario;
            _usuario = usuario;
        }

        public Veterinario BuscarVeterinarioId(int id)
        {
            return _veterinario.buscar(id);
        }

        public Veterinario ObtenerVeterinarioPorId(int idVeterinario)
        {
            return _veterinario.ObtenerVeterinarioPorId(idVeterinario);
        }

        public string AgregarVeterinario(VeterinarioViewModel model)
        {
            string mensaje = "";

            try
            {
                Usuario user = new Usuario
                {
                    email = model.email,
                    password = model.password,
                    idrol = Roles.VETERINARIO
                };

                int idUsuario = _usuario.InsertarUsuario(user);
                if (idUsuario <= 0)
                    return "Error al registrase";

                Veterinario veterinario = new Veterinario
                {
                    idusuario = idUsuario,
                    nombre = model.nombre,
                    apellido = model.apellido,
                };

                mensaje = _veterinario.agregar(veterinario);


                return mensaje;

            }
            catch (Exception ex) { mensaje = ex.Message; }
            return mensaje;
        }

        public ResumenVeterinarioViewModel ObtenerResumen(int idVeterinario)
        {
            return _veterinario.ObtenerResumen(idVeterinario);
        }
    }
}
