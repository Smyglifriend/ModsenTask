using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using ModsenTask.DataAccess.Domain.Abstraction.Interfaces;

namespace ModsenTask.DataAccess.Domain.Models;

public class User : IdentityUser<long>, IEntity
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string ActualRefreshToken { get; set; }


    public IEnumerable<UserRole> UserRoles { get; set; }
}