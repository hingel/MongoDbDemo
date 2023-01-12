namespace DataAccess;

public interface IRepository<T>
{
    void Add(T entity);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetByMake(string make);
    void Replace(object id, T item);
    void Delete(object id);
}