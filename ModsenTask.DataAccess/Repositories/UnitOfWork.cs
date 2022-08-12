using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ModsenTask.DataAccess.Abstractions.Repostories;
using ModsenTask.DataAccess.Domain.Abstraction.Interfaces;

namespace ModsenTask.DataAccess.Repositories;

internal class UnitOfWork<TContext>
    : IUnitOfWork
    where TContext : DbContext
{
    private bool _disposed;
    private IDictionary<Type, object> _readonlyRepositories;
    private IDictionary<Type, object> _readwriteRepositories;

    public TContext DbContext { get; }


    public UnitOfWork(TContext context)
    {
        DbContext = context
            ?? throw new ArgumentNullException(nameof(context));
    }


    public IBaseReadonlyRepository<TEntity> GetReadonlyRepository<TEntity>()
        where TEntity : class
    {
        if (_readonlyRepositories == null)
        {
            _readonlyRepositories = new Dictionary<Type, object>();
        }

        var type = typeof(TEntity);
        if (!_readonlyRepositories.ContainsKey(type))
        {
            _readonlyRepositories[type] = new BaseReadonlyRepository<TEntity>(DbContext);
        }

        return (IBaseReadonlyRepository<TEntity>) _readonlyRepositories[type];
    }

    public IBaseReadWriteRepository<TEntity> GetReadWriteRepository<TEntity>()
        where TEntity : class, IEntity
    {
        if (_readwriteRepositories == null)
        {
            _readwriteRepositories = new Dictionary<Type, object>();
        }

        var type = typeof(TEntity);
        if (!_readwriteRepositories.ContainsKey(type))
        {
            _readwriteRepositories[type] = new BaseReadWriteRepository<TEntity>(DbContext);
        }

        return (IBaseReadWriteRepository<TEntity>)_readwriteRepositories[type];
    }

    public TIRepository GetCustomRepository<TEntity, TIRepository>()
        where TEntity : class
        where TIRepository : class, IBaseReadonlyRepository<TEntity>
    {
        var customRepo = DbContext.GetService<TIRepository>();

        return customRepo;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await DbContext.SaveChangesAsync();
    }
}