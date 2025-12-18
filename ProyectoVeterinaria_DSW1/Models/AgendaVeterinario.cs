using System.ComponentModel.DataAnnotations;

namespace ProyectoVeterinaria_DSW1.Models
{
    public class AgendaVeterinario
    {
        public int IdAgenda { get; set; }

        public int IdVeterinario { get; set; }

        [Required(ErrorMessage = "El Día de la Semana es obligatorio.")]
        [Range(1, 6, ErrorMessage = "Día inválido. Debe ser de Lunes (1) a Sábado (6).")]
        public int DiaSemana { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "El cupo máximo es obligatorio.")]
        [Range(0, 10, ErrorMessage = "El cupo debe estar entre 0 y 10.")]
        public int CupoMaximo { get; set; }

        public int CupoDisponible { get; set; }

        public string NombreDiaSemana =>
            DiaSemana switch
            {
                1 => "Lunes",
                2 => "Martes",
                3 => "Miércoles",
                4 => "Jueves",
                5 => "Viernes",
                6 => "Sábado",
                _ => "Día Inválido"
            };
    }
}
