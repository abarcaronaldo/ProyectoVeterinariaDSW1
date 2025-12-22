namespace ProyectoVeterinaria_DSW1.Models
{
    public class Paginado<T>
    {
        public int Total { get; set; }
        public List<T> Items { get; set; } = new();
    }
}
