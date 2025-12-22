namespace ProyectoVeterinaria_DSW1.ViewsModel
{
    public class ReporteCitaSemanalVM
    {
        public int IdCita { get; set; }
        public DateOnly FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string? Motivo { get; set; }
        public string? Mascota { get; set; }
        public string? Dueno { get; set; }
        public int IdEstado { get; set; }
    }
}
