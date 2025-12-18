namespace ProyectoVeterinaria_DSW1.ViewsModel
{
    public class CitaListadoViewModel
    {
        public int IdCita { get; set; }

        public DateOnly FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }

        public string? Motivo { get; set; }

        public DateOnly FechaCreacion { get; set; }

        //mascota
        public int IdMascota { get; set; }
        public string? NombreMascota { get; set; }

        //estado
        public int IdEstado { get; set; }
        public string? EstadoCita { get; set; }

        public int IdAgenda { get; set; }
    }
}
