namespace ProyectoVeterinaria_DSW1.Models
{
    public class Agenda
    {
        public int IdAgenda { get; set; }
        public int IdVeterinario { get; set; }
        public int DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int CupoMaximo { get; set; }
        public int CupoDisponible { get; set; }
    }
}
