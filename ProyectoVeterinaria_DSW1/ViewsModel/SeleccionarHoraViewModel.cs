namespace ProyectoVeterinaria_DSW1.ViewsModel
{
    public class SeleccionarHoraViewModel
    {
        public int IdAgenda { get; set; }
        public DateOnly FechaCita { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int DuracionCitaMin { get; set; }
        public string? HoraDeseada { get; set; }
    }
}
