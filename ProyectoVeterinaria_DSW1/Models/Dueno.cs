using System.ComponentModel.DataAnnotations;

namespace ProyectoVeterinaria_DSW1.Models
{
    public class Dueno
    {
        public int idueno { get; set; }
        public int idusuario { get; set; }
        [Display(Name = "Nombre")]

        public string? nombre { get; set; }
        [Display(Name = "Apellido")]
        public string? apellido { get; set; }
        [Display(Name = "Telefono")]
        public string? telefono { get; set; }
        [Display(Name = "Direccion")]
        public string? direccion { get; set; }
    }
}
