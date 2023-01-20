namespace DataAccess;

public interface IRepository<T>
{
    void Add(T entity);
    IEnumerable<T> GetAllItems();
    T GetById(object id);
    void Replace(object id, T item);
    void Delete(object id);
}