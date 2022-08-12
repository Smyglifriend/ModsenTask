namespace ModsenTask.Services.Auth.Abstractions.Models.Tokens;

public class AccessToken
{
    public string AccessTokenString { get; set; }

    public string ExpiresAt { get; set; }
}