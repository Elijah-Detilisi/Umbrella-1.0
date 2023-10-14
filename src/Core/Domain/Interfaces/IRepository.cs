namespace Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<int> Save(T entity);
    Task<int> Remove(T entity);
    Task<T> FindById(Guid id);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> spec);
}  