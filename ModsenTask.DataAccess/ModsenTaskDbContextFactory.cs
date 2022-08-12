using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ModsenTask.DataAccess;

public class ModsenTaskDbContextFactory : IDesignTimeDbContextFactory<ModsenTaskDbContext>
{
    private const string CONNECTING_STRING = "Server=(LocalDb)\\MSSQLLocalDB;Database=ModsenTask;Trusted_Connection=True";

    public ModsenTaskDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ModsenTaskDbContext>();
        optionsBuilder.UseSqlServer(CONNECTING_STRING);

        return new ModsenTaskDbContext(optionsBuilder.Options);
    }
}