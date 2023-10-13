using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    void Save(TEntity entity);
    void Remove(TEntity entity);
    TEntity FindById(Guid id);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> spec);
}