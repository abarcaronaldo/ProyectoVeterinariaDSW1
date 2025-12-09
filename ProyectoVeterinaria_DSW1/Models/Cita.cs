namespace ProyectoVeterinaria_DSW1.Models
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdMascota { get; set; }
        public DateOnly FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public String? Motivo { get; set; }
        public int IdAgenda { get; set; }
        public int IdDueno { get; set; }
    }
}
