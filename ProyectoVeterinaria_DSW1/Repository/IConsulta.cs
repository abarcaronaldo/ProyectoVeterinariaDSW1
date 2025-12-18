namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface IConsulta<T> where T:class
    {
        IEnumerable<T> listado();
        T buscar(int id);
    }
}
