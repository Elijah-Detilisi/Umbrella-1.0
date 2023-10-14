namespace Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<int> Save(TEntity entity);
    Task<int> Remove(TEntity entity);
    Task<TEntity> FindById(Guid id);
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> spec);
}  