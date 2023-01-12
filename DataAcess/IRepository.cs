namespace DataAccess;

public interface IRepository<T>
{
    void Add(T entity);
    IEnumerable<T> GetAllCars();
    IEnumerable<T> GetBySpec(T item);
    void Replace(object id, T item);
    void Delete(object id);
}