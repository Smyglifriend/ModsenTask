namespace ModsenTask.Services.Auth.Abstractions.Models.Tokens;

public class RefreshToken
{
    public string RefreshTokenString { get; set; }

    public string ExpiresAt { get; set; }
}