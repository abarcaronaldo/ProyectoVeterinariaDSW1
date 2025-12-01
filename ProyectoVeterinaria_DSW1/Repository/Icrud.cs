namespace ProyectoVeterinaria_DSW1.Repository
{
    public interface Icrud<T> where T:class
    {
        string agregar(T entidad);
        string actualizar(T entidad);
        string eliminar(T entidad);
        T buscar(int id);
    }
}
