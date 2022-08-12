using System.Linq.Expressions;

namespace ModsenTask.DataAccess.Abstractions.Repostories;

public interface IBaseReadonlyRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task<IEnumerable<TEntity>> GetAllAsync(
        params Expression<Func<TEntity, object>>[] includes);

    Task<IEnumerable<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter = null);

    Task<IEnumerable<TEntity>> GetWhereAsync(
        Expression<Func<TEntity, bool>> filter = null,
        params Expression<Func<TEntity, object>>[] includes);

    Task<TEntity> GetSingleOrDefaultAsync(
        Expression<Func<TEntity, bool>> filter);

    Task<TEntity> GetSingleOrDefaultAsync(
        Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includes);

    Task<TEntity> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> filter);

    Task<TEntity> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> filter,
        params Expression<Func<TEntity, object>>[] includes);
}