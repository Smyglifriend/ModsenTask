using Microsoft.AspNetCore.Identity;
using ModsenTask.DataAccess.Abstractions.Repostories;
using ModsenTask.DataAccess.Domain.Models;

namespace ModsenTask.DataAccess.Stores;

public class RoleStore : IRoleStore<Role>
{
    private readonly IUnitOfWork _unitOfWork;


    public RoleStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<Role>();
        await repository.SaveAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<Role>();
        await repository.SaveAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<Role>();
        await repository.RemoveAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken = default)
        => Task.FromResult(role.Id.ToString());

    public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken = default)
        => Task.FromResult(role.Name);

    public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken = default)
    {
        role.Name = roleName;

        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken = default)
        => Task.FromResult(role.NormalizedName);

    public Task SetNormalizedRoleNameAsync(
        Role role,
        string normalizedName,
        CancellationToken cancellationToken = default)
    {
        role.NormalizedName = normalizedName;

        return Task.CompletedTask;
    }

    public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        var intRoleId = int.Parse(roleId);
        var role = (await _unitOfWork
            .GetReadWriteRepository<Role>()
            .GetFirstOrDefaultAsync(role => role.Id == intRoleId));

        return role;
}

    public async Task<Role> FindByNameAsync(string roleName, CancellationToken cancellationToken = default)
        => (await _unitOfWork
            .GetReadonlyRepository<Role>()
            .GetFirstOrDefaultAsync(role => role.Name == roleName));

    public void Dispose()
    {
        // is not needed
    }
}