using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModsenTask.DataAccess.Abstractions.Repostories;
using ModsenTask.DataAccess.Domain.Models;
using ModsenTask.DataAccess.Repositories;

namespace ModsenTask.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ModsenTaskDbContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("ModsenTask")));

    public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
        where TContext : DbContext
        => services
            .AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
    
    public static IServiceCollection AddCustomRepositories(this IServiceCollection services)
        => services
            .AddCustomRepository<UserRole, IUserRoleRepository, UserRoleRepository>();


    private static IServiceCollection AddCustomRepository<TEntity, TIRepository, TRepository>(
        this IServiceCollection services)
        where TEntity : class
        where TIRepository : class, IBaseReadonlyRepository<TEntity>
        where TRepository : class, TIRepository
        => services
            .AddScoped<TIRepository, TRepository>();
}