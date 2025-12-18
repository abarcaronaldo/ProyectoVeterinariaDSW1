using System.ComponentModel.DataAnnotations;

namespace ProyectoVeterinaria_DSW1.ViewsModel
{
    public class EditarCitaViewModel
    {
        public int IdCita { get; set; }
        public int IdMascota { get; set; }
        public string? Motivo { get; set; }
    }
}
