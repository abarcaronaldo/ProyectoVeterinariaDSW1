using ProyectoVeterinaria_DSW1.Constants;
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

        public Dueno BuscarDuenoId(int id)
        {
            return _dueno.buscar(id);

        }

        public Dueno ObtenerDuenoPorId(int idDueno)
        {
            return _dueno.BuscarDuenoPorId(idDueno);
        }

        public string RegistrarDueno(DuenoViewModel model)
        {
            string mensaje = "";

            try
            {
                Usuario user = new Usuario
                {
                    email = model.email,
                    password = model.password,
                    idrol = Roles.DUENO
                };

                int idUsuario = _usuario.InsertarUsuario(user);
                if (idUsuario <= 0)
                    return "Error al registrase";

                Dueno dueno = new Dueno
                {
                    idusuario = idUsuario,
                    nombre = model.nombre,
                    apellido = model.apellido,
                    telefono = model.telefono,
                    direccion = model.direccion
                };

                mensaje = _dueno.agregar(dueno);


                return mensaje;

            }
            catch (Exception ex){mensaje = ex.Message;}
            return mensaje;
        }
    }
}
