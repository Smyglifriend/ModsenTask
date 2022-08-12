using System.Security.Claims;
using ModsenTask.DataAccess.Domain.Models;

namespace ModsenTask.Services.Auth.Abstractions.Interfaces;

public interface IUserService
{
    Task<List<Claim>> GetClaims(User user);

}