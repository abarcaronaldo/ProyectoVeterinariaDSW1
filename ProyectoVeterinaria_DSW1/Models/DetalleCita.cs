namespace ProyectoVeterinaria_DSW1.Models
{
    public class DetalleCita
    {
        public int idcita { get; set; }
        public DateTime fechacita { get; set; }
        public TimeSpan horacita { get; set; }
        public string motivo { get; set; }
        public DateTime fechacreacion { get; set; }
        public string estado { get; set; }
        public string nombremascota { get; set; }
        public string especie { get; set; }
        public string raza { get; set; }
        public string nombredueno { get; set; }
        public string apellidodueno { get; set; }
        public string telefonodueno { get; set; }
    }
}
