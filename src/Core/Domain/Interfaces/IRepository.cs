namespace Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    T GetById(Guid id);
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
    IEnumerable<T> List();
    Task<IEnumerable<T>> Get(Expression<Func<T, bool>> spec);

}  