namespace Bookstore.Common;

public interface ICrudService<T>
{
    void Add(T item);
    List<T> GetAll();
    T? GetById(int id);
    bool Update(int id, T updatedItem);
    bool Delete(int id);
}