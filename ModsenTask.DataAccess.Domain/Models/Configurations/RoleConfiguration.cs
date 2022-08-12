using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModsenTask.Core.Abstractions.Constants;

namespace ModsenTask.DataAccess.Domain.Models.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .Property(u => u.Name)
            .IsRequired();
        builder
            .HasIndex(u => u.Name)
            .IsUnique();

        builder
            .Property(u => u.NormalizedName)
            .IsRequired();
        builder
            .HasIndex(u => u.NormalizedName)
            .IsUnique();
        
        builder
            .HasData(new Role
            {
                Id = 1,
                Name = RoleConstants.ROLE_USER,
                NormalizedName = RoleConstants.ROLE_USER.ToUpper(),
                ConcurrencyStamp = "7612cd22-c0f0-4801-a3e5-ff7cd1a41302"
            });

        builder
            .HasData(new Role
            {
                Id = 2,
                Name = RoleConstants.ROLE_ADMIN,
                NormalizedName = RoleConstants.ROLE_ADMIN.ToUpper(),
                ConcurrencyStamp = "7612cd22-c0f0-4801-a3e5-ff7cd1a41301"
            });
    }
}