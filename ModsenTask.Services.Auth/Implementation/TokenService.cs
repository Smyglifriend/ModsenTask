using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ModsenTask.Core.Abstractions.Helpers;
using ModsenTask.DataAccess.Domain.Models;
using ModsenTask.DataAccess.Extensions;
using ModsenTask.Services.Auth.Abstractions.Interfaces;
using ModsenTask.Services.Auth.Abstractions.Models;
using ModsenTask.Services.Auth.Abstractions.Models.Tokens;
using TokenOptions = ModsenTask.Services.Auth.Abstractions.Models.Tokens.TokenOptions;

namespace ModsenTask.Services.Auth.Implementation;

public class TokenService : ITokenService
{
    private UserManager<User> _userManager;
    private IConfigurationHelper _configurationHelper;
    private IUserService _userService;


    public TokenService(
        UserManager<User> userManager,
        IConfigurationHelper configurationHelper,
        IUserService userService)
    {
        _userManager = userManager;
        _configurationHelper = configurationHelper;
        _userService = userService;
    }

    public async Task<TokenDto> RefreshAsync(string refreshTokenString)
    {
        var decodedToken = DecodeToken<RefreshTokenDto>(refreshTokenString);
        var user = await _userManager.FindByIdAsync(decodedToken.UserId.ToString());
        var decodedActualUserRefreshToken = DecodeToken<RefreshTokenDto>(user.ActualRefreshToken);
        if (decodedToken.IsExpired
            || user is null
            || decodedActualUserRefreshToken.IsExpired
            || refreshTokenString != user.ActualRefreshToken)
            throw new Exception("Invalid refresh token");
        var refreshToken = await GenerateRefreshTokenAsync(user);
        var accessToken = await SetAccessTokenAsync(user);
        return new()
        {
            RefreshToken = refreshToken.RefreshTokenString,
            AccessToken = accessToken.AccessTokenString,
            RefreshTokenExpiresAt = refreshToken.ExpiresAt,
            AccessTokenExpiresAt = accessToken.ExpiresAt
        };
    }

    public async Task<RefreshToken> GenerateRefreshTokenAsync(User user)
    {
        var refreshToken = new RefreshTokenDto
        {
            UserId = user.Id,
            ExpiresInMinutes = DateTime.Now.AddMinutes(30)
        };

        var encodedToken = await _userManager.SetActiveRefreshTokenAsync(user, EncodeToken(refreshToken));
        return new RefreshToken
        {
            RefreshTokenString = encodedToken,
            ExpiresAt = refreshToken.ExpiresInMinutes.ToString("yyyy-MM-ddTHH:mm:ss")
        };
    }

    public TToken DecodeToken<TToken>(string token)
    {
        var base64EncodeBytes = Convert.FromBase64String(token);

        return JsonSerializer.Deserialize<TToken>(
            Encoding.UTF8.GetString(base64EncodeBytes));
    }

    public string EncodeToken(object tokenDto)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(tokenDto));

        return Convert.ToBase64String(plainTextBytes);
    }

    public async Task<AccessToken> SetAccessTokenAsync(User user)
    {
        var signinCredentials = GetSigninCredentials();
        var claims = await _userService.GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signinCredentials, claims);
        var encodedAccessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions.JwtSecurityToken);

        return new AccessToken
        {
            AccessTokenString = encodedAccessToken,
            ExpiresAt = tokenOptions.ExpiresAt
        };
    }


    private SigningCredentials GetSigninCredentials()
    {
        var jwtConfig = _configurationHelper.JwtConfig;
        var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private TokenOptions GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configurationHelper.JwtConfig;
        var expireTime = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresIn"]));
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["ValidIssuer"],
            audience: jwtSettings["ValidAudience"],
            claims: claims,
            expires: expireTime,
            signingCredentials: signingCredentials
        );
        return new TokenOptions()
        {
            JwtSecurityToken = tokenOptions,
            ExpiresAt = expireTime.ToString("yyyy-MM-ddTHH:mm:ss")
        };
    }
}