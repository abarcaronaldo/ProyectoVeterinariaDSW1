using System.ComponentModel.DataAnnotations;

namespace ProyectoVeterinaria_DSW1.ViewsModel
{
    public class VeterinarioViewModel
    {
        //usuario
        [Display(Name = "Correo Electronico")]
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        public string? email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Debes agregar una contraseña")]
        public string? password { get; set; }

        //veterinario
        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string? nombre { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string? apellido { get; set; }
    }
}
