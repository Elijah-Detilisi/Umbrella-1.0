namespace Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    void Save();
    Task<int> Add(T entity);
    Task<int> Update(T entity);
    Task<int> Delete(T entity);
    Task<T> GetById(Guid id);
    Task<IEnumerable<T>> Get(Expression<Func<T, bool>> spec);

}  