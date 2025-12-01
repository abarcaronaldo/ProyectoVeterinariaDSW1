using ProyectoVeterinaria_DSW1.Models;
using ProyectoVeterinaria_DSW1.Repository;
using ProyectoVeterinaria_DSW1.ViewsModel;

namespace ProyectoVeterinaria_DSW1.Services
{
    public class DuenoService
    {

        IUsuario _usuario;
        IDueno _dueno;
        public DuenoService(IUsuario usuario, IDueno dueno)
        {
            _usuario = usuario;
            _dueno = dueno;
        }

        public string RegistrarDueno(RegistroViewModel model)
        {
            string mensaje = "";

            Usuario user = new Usuario
            {
                email = model.email,
                password = model.password,
                idrol = 1
            };

            int idUsuario = _usuario.ObtenerID(user);

            Dueno dueno = new Dueno
            {
                idusuario = idUsuario,
                nombre = model.nombre,
                apellido=model.apellido,
                telefono=model.telefono,
                direccion=model.direccion
            };

            mensaje = _dueno.agregar(dueno);


            return mensaje;
        }
    }
}
