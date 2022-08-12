using Microsoft.AspNetCore.Identity;
using ModsenTask.DataAccess.Abstractions.Repostories;
using ModsenTask.DataAccess.Domain.Models;

namespace ModsenTask.DataAccess.Stores;

public class UserStore : IUserRoleStore<User>, IUserEmailStore<User>, IUserPasswordStore<User>
{
    private readonly IUnitOfWork _unitOfWork;


    public UserStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.Id.ToString());

    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.UserName);

    public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken = default)
    {
        user.UserName = userName;

        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.NormalizedUserName);
    
    public Task SetNormalizedUserNameAsync(
        User user,
        string normalizedName,
        CancellationToken cancellationToken = default)
    {
        user.NormalizedUserName = normalizedName;

        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<User>();
        await repository.SaveAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<User>();
        await repository.SaveAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<User>();
        await repository.RemoveAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var id = long.Parse(userId);

        return (await _unitOfWork
            .GetReadonlyRepository<User>()
            .GetFirstOrDefaultAsync(u =>u.Id == id));
    }

    public async Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return (await _unitOfWork
                .GetReadonlyRepository<User>()
                .GetFirstOrDefaultAsync(u =>u.UserName == userName));
    }

    public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        var rolesRepository = _unitOfWork.GetReadonlyRepository<Role>();
        var userRolesRepository = _unitOfWork.GetCustomRepository<UserRole, IUserRoleRepository>();
        var role = (await rolesRepository.GetSingleOrDefaultAsync(r=>r.Name == roleName));

        var userRole = new UserRole
        {
            RoleId = role.Id,
            UserId = user.Id
        };

        await userRolesRepository.SaveAsync(userRole);
    }

    public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetCustomRepository<UserRole, IUserRoleRepository>();
        var userRole = (await userRolesRepository.GetFirstOrDefaultAsync(
            userRole => userRole.Role.Name == roleName && userRole.UserId == user.Id));

        await userRolesRepository.RemoveAsync(userRole);
    }

    public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetCustomRepository<UserRole, IUserRoleRepository>();
        var userRoles = await userRolesRepository.GetWhereAsync(
            userRole => userRole.UserId == user.Id,
            ur => ur.Role);
        var roleNames = userRoles.Select(userRole => userRole.Role.Name).ToList();

        return roleNames;
    }

    public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetCustomRepository<UserRole, IUserRoleRepository>();
        var isInRole = (await userRolesRepository.GetWhereAsync(
            userRole => userRole.Role.Name == roleName && userRole.UserId == user.Id)).Any();

        return isInRole;
    }

    public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetCustomRepository<UserRole, IUserRoleRepository>();
        var userRoles = await userRolesRepository.GetAllAsync(
            userRole => userRole.Role.NormalizedName == roleName,
            null,
            ur => ur.User);
        var users = userRoles.Select(userRole => userRole.User).ToList();

        return users;
    }

    public void Dispose()
    {
        // is not needed
    }

    public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken = default)
    {
        user.Email = email;

        return Task.CompletedTask;
    }

    public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.Email);

    public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.EmailConfirmed);

    public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken = default)
    {
        user.EmailConfirmed = confirmed;

        return Task.CompletedTask;
    }

    public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var userRepository = _unitOfWork.GetReadonlyRepository<User>();
        var user = await userRepository.GetFirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        => Task.FromResult(user.NormalizedEmail);

    public Task SetNormalizedEmailAsync(
        User user,
        string normalizedEmail,
        CancellationToken cancellationToken = default)
    {
        user.NormalizedEmail = normalizedEmail;

        return Task.CompletedTask;
    }

    public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken = default)
    {
        user.PasswordHash = passwordHash;

        return Task.CompletedTask;
    }

    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.PasswordHash);
    
    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.PasswordHash != null);
}