using System.ComponentModel.DataAnnotations;

namespace ProyectoVeterinaria_DSW1.Models
{
    public class HistorialMedico
    {
        public int IdHistorial { get; set; }
        public int IdCita { get; set; }
        public int IdMascota { get; set; }
        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "El Diagnóstico es obligatorio.")]
        [StringLength(500, ErrorMessage = "El diagnóstico no debe exceder 500 caracteres.")]
        public string Diagnostico { get; set; }

        [Required(ErrorMessage = "El Tratamiento es obligatorio.")]
        [StringLength(500, ErrorMessage = "El tratamiento no debe exceder 500 caracteres.")]
        public string Tratamiento { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no deben exceder 500 caracteres.")]
        public string Observaciones { get; set; } = string.Empty;
        public DateTime FechaAtencion { get; set; }
        public string NombreMascota { get; set; } = string.Empty;
        public string NombreDueno { get; set; } = string.Empty;
    }
}
