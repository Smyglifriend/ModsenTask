using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ModsenTask.DataAccess.Domain.Models;
using ModsenTask.Services.Auth.Abstractions.Interfaces;

namespace ModsenTask.Services.Auth.Implementation;

public class UserService : IUserService
{
    private UserManager<User> _userManager;


    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }


    public async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new("Id", user.Id.ToString())
        };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        return claims;
    }
}