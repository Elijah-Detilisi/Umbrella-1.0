using Domain.Common.Base;
using Persistence.Data;

namespace Persistence.Repositories;

public class EntityRepository<TEntity> where TEntity : Entity
{
    private readonly AppDbContext _context;
    
    public EntityRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public async Task<int> InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<int> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<TEntity?> GetById(int id)
    {
        var result = await _context.Set<TEntity>()
            .SingleOrDefaultAsync(x=> x.Id == id);

        return result;
    }
}
