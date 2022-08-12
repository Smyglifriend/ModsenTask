using Microsoft.AspNetCore.Identity;
using ModsenTask.DataAccess.Domain.Models;

namespace ModsenTask.DataAccess.Extensions;

public static class UserManagerExtensions
{
    public static async Task<string> SetActiveRefreshTokenAsync(
        this UserManager<User> userManager,
        User user,
        string refreshToken)
    {
        user.ActualRefreshToken = refreshToken;
        await userManager.UpdateAsync(user);

        return refreshToken;
    }
}