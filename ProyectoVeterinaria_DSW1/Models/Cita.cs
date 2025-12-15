namespace ProyectoVeterinaria_DSW1.Models
{
    public class Cita
    {
        public int idcita { get; set; }
        public DateTime fechacita {  get; set; }
        public TimeSpan horacita {  get; set; }
        public string estado { get; set; }
        public string especie { get; set; }
        public string nombredueno { get; set; }
    }
}
