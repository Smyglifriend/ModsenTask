using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModsenTask.Core.Abstractions.Constants;

namespace ModsenTask.DataAccess.Domain.Models.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.ActualRefreshToken)
            .IsRequired(false);

        builder
            .Property(u => u.Name)
            .IsRequired(false);

        builder
            .Property(u => u.Surname)
            .IsRequired(false);

        builder
            .Property(u => u.MiddleName)
            .IsRequired(false);

        builder
            .HasData(new User
            {
                Id = 1,
                UserName = UserConstants.ADMIN_NAME,
                NormalizedUserName = UserConstants.ADMIN_NAME.ToUpper(),
                Email = UserConstants.ADMIN_EMIAL,
                NormalizedEmail = UserConstants.ADMIN_EMIAL.ToUpper(),
                PasswordHash = GetPasswordHash(UserConstants.ADMIN_PASSWORD),
                ConcurrencyStamp = "85263788-277f-4f89-b8c4-a11ac465ed58"
            });
    }


    private static string GetPasswordHash(string password)
    {
        var user = new User();
        var passwordHasher = new PasswordHasher<User>();
        var passwordHash = passwordHasher.HashPassword(user, password);

        return passwordHash;
    }
}