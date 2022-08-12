using Microsoft.AspNetCore.Identity;
using ModsenTask.Core.Helpers;
using ModsenTask.DataAccess.Domain.Models;

namespace ModsenTask.Core.Extensions;

public static class IdentityBuilderExtension
{
    public static IdentityBuilder AddDefaultTokenProvider(this IdentityBuilder builder)
        => builder
            .AddTokenProvider("Default", typeof(EmailConfirmationTokenProvider<User>));
}