namespace ModsenTask.Services.Auth.Abstractions.Models;

public class RefreshTokenDto
{
    public long UserId { get; set; }

    public DateTime ExpiresInMinutes { get; set; }

    public bool IsExpired => DateTime.Now > ExpiresInMinutes;
}