namespace ProyectoVeterinaria_DSW1.Models
{
    public class Usuario
    {
        public int idusuario { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public int? idrol { get; set; } //fk
        public DateTime FechaRegistro { get; set; }
    }
}
