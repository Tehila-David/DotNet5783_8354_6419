using DO;

namespace DalApi
{
    public interface ICrud<T> where T: struct
    {
        int Add(T item);
        T GetById(int id);
        T GetById(Func<T?, bool>? predicate);
        void Update(T item); 
        void Delete(int id);    
        IEnumerable<T?> GetAll(Func<T?, bool>? predicate = null);   
    }
}
