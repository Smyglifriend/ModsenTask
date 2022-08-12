using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ModsenTask.DataAccess.Abstractions.Repostories;

namespace ModsenTask.DataAccess.Repositories;

internal class BaseReadonlyRepository<TEntity>
    : IBaseReadonlyRepository<TEntity>
    where TEntity : class
{
    protected DbContext _dbContext;
    protected DbSet<TEntity> _dbSet;


    public BaseReadonlyRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }


    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        IQueryable<TEntity> query = _dbSet;

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.SingleOrDefaultAsync();
    }

    public async Task<TEntity> GetSingleOrDefaultAsync(
        Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.SingleOrDefaultAsync();
    }

    public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
    {
        IQueryable<TEntity> query = _dbSet;
        query = query.Where(filter);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<TEntity> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync();
    }
}