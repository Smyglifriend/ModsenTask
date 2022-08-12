namespace ModsenTask.Services.Auth.Abstractions.Models;

public class TokenDto
{
    public string RefreshToken { get; set; }

    public string AccessToken { get; set; }
    
    public string RefreshTokenExpiresAt { get; set; }

    public string AccessTokenExpiresAt { get; set; }
}