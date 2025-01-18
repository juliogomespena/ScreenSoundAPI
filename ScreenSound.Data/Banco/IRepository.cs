namespace ScreenSound.Banco;

public interface IRepository<T> where T : class
{
    T? FindByParameter(Func<T, bool> predicate);
    IEnumerable<T> ListAll();
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
}