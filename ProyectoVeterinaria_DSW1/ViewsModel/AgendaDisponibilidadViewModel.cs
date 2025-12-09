namespace ProyectoVeterinaria_DSW1.ViewsModel
{
    public class AgendaDisponibilidadViewModel
    {
        public int IdAgenda { get; set; }
        public int IdVeterinario { get; set; }
        public string? NombreVeterinario { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int CupoMaximo { get; set; }
        public int CupoDisponible { get; set; }
    }
}
