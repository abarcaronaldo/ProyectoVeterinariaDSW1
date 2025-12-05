namespace ProyectoVeterinaria_DSW1.Models
{
    public class HistorialMedico
    {
        public int idhistorial {  get; set; }
        public int idmascota { get; set; }
        public int idveterinario { get; set; }
        public int idcita { get; set; }
        public string fechaatencion { get; set; }
        public string diagnostico { get; set; }
        public string tratamiento { get; set; }
        public string ? observaciones { get; set; }
    }
}
