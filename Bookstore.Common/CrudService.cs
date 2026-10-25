namespace Bookstore.Common;

public class CrudService<T> : ICrudService<T> where T : Publication
{
    private readonly List<T> _items = new();

    public void Add(T item)
    {
        _items.Add(item);
    }

    public List<T> GetAll()
    {
        return _items;
    }

    public T? GetById(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(int id, T updatedItem)
    {
        var index = _items.FindIndex(x => x.Id == id);
        if (index == -1) return false;

        _items[index] = updatedItem;
        return true;
    }

    public bool Delete(int id)
    {
        var item = GetById(id);
        if (item == null) return false;

        return _items.Remove(item);
    }
}