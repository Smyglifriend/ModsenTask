using Microsoft.AspNetCore.Identity;
using ModsenTask.DataAccess.Domain.Abstraction.Interfaces;

namespace ModsenTask.DataAccess.Domain.Models;

public class Role : IdentityRole<long>, IEntity
{
    public IEnumerable<UserRole> UserRoles { get; set; }
}