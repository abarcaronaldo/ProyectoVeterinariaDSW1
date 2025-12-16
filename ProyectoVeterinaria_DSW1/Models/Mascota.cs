using System.ComponentModel.DataAnnotations;

namespace ProyectoVeterinaria_DSW1.Models
{
    public class Mascota
    {
        [Display(Name = "Id Mascota")]
        public int idmascota { get; set; }
        public int iddueno { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres.")]
        public string? nombre { get; set; }

        [Display(Name = "Especie")]
        [Required(ErrorMessage = "La especie es obligatoria.")]
        public string? especie { get; set; }

        [Display(Name = "Raza Mascota")]
        [Required(ErrorMessage = "La raza es obligatoria.")]
        public string? raza { get; set; }

        [Display(Name = "Fecha Nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateOnly fechanacimiento { get; set; }

        [Display(Name = "Peso")]
        [Required(ErrorMessage = "El peso es obligatorio.")]
        [Range(0.1, 100, ErrorMessage = "El peso debe estar entre 0.1 y 100 kg.")]
        public decimal peso { get; set; }

    }
}
