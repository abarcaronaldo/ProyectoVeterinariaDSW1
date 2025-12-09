namespace ProyectoVeterinaria_DSW1.ViewsModel
{
    public class RegistrarCitaViewModel
    {
        public int IdAgenda { get; set; }
        public int IdMascota { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string? Motivo { get; set; }
    }
}
