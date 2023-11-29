using Domain.Common.Base;

namespace Application.Common.Repositories;

public interface IEntityRepository<TEntity> where TEntity : Entity
{
    Task<int> DeleteAsync(TEntity entity);
    List<TEntity> GetAll();
    Task<TEntity?> GetById(int id);
    Task<int> InsertAsync(TEntity entity);
    Task<int> UpdateAsync(TEntity entity);
}

