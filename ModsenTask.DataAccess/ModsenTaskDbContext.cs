using Microsoft.EntityFrameworkCore;
using ModsenTask.DataAccess.Domain.Extensions;

namespace ModsenTask.DataAccess;

public class ModsenTaskDbContext : DbContext
{
    public ModsenTaskDbContext()
    {
        
    }
    public ModsenTaskDbContext(DbContextOptions<ModsenTaskDbContext> options)
        :base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyEntities();
    }
}