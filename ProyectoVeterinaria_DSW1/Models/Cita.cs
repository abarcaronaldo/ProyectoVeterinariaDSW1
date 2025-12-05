namespace ProyectoVeterinaria_DSW1.Models
{
    public class Cita
    {
        public int idcita { get; set; }
        public int idmascota { get; set; }
        public int idveterinario { get; set; }
        public DateTime fechacita {  get; set; }
        public DateTime horacita {  get; set; }
        public string motivo { get; set; }
        public string estado { get; set; }
        public DateTime fechacreacion { get; set; }
    }
}
