using Microsoft.EntityFrameworkCore;
using ModsenTask.DataAccess.Abstractions.Repostories;
using ModsenTask.DataAccess.Domain.Abstraction.Interfaces;

namespace ModsenTask.DataAccess.Repositories;

internal class BaseReadWriteRepository<TEntity>
    : BaseReadonlyRepository<TEntity>, 
        IBaseReadWriteRepository<TEntity>
    where TEntity : class, IEntity
{
    public BaseReadWriteRepository(DbContext dbContext) : base(dbContext)
    {
    }


    public virtual async Task<long> SaveAsync(TEntity model)
    {
        if (model.Id > 0)
        {
            _dbSet.Update(model);
        }
        else
        {
            await _dbSet.AddAsync(model);
        }

        await _dbContext.SaveChangesAsync();
        
        return model.Id;
    }

    public virtual async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task RemoveAsync(TEntity model)
    {
        _dbContext.Remove(model);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task RemoveAsync(long id)
    {
        var model = await _dbSet.SingleOrDefaultAsync(x => x.Id == id);

        await RemoveAsync(model);
    }
}