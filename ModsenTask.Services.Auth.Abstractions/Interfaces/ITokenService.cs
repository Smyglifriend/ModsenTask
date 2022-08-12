using ModsenTask.DataAccess.Domain.Models;
using ModsenTask.Services.Auth.Abstractions.Models;
using ModsenTask.Services.Auth.Abstractions.Models.Tokens;

namespace ModsenTask.Services.Auth.Abstractions.Interfaces;

public interface ITokenService
{

    Task<TokenDto> RefreshAsync(string refreshToken);

    Task<RefreshToken> GenerateRefreshTokenAsync(User user);

    TToken DecodeToken<TToken>(string token);

    string EncodeToken(object tokenDto);

    Task<AccessToken> SetAccessTokenAsync(User user);

}