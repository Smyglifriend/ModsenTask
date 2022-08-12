using System.IdentityModel.Tokens.Jwt;

namespace ModsenTask.Services.Auth.Abstractions.Models.Tokens;

public class TokenOptions
{
    public JwtSecurityToken JwtSecurityToken { get; set; }

    public string ExpiresAt { get; set; }
}