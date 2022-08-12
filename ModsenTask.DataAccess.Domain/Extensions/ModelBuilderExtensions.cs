using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ModsenTask.DataAccess.Domain.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyEntities(this ModelBuilder builder)
        => builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}