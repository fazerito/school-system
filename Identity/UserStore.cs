using SchoolProject.Models;

namespace SchoolProject
{
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

public class UserStore : IUserStore<Users>, IUserPasswordStore<Users>
{
    private readonly SchoolDbContext db;

    public UserStore(SchoolDbContext db)
    {
        this.db = db;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            db?.Dispose();
        }
    }

    public Task<string> GetUserIdAsync(Users user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserId.ToString());
    }

    public Task<string> GetUserNameAsync(Users user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Login);
    }

    public Task SetUserNameAsync(Users user, string userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(nameof(SetUserNameAsync));
    }

    public Task<string> GetNormalizedUserNameAsync(Users user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(nameof(GetNormalizedUserNameAsync));
    }

    public Task SetNormalizedUserNameAsync(Users user, string normalizedName, CancellationToken cancellationToken)
    {
        return Task.FromResult((object)null);
    }

    public async Task<IdentityResult> CreateAsync(Users user, CancellationToken cancellationToken)
    {
        db.Add(user);

        await db.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> UpdateAsync(Users user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(nameof(UpdateAsync));
    }

    public async Task<IdentityResult> DeleteAsync(Users user, CancellationToken cancellationToken)
    {
        db.Remove(user);

        int i = await db.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
    }

    public async Task<Users> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        if (int.TryParse(userId, out int id))
        {
            return await db.Users.FindAsync(id);
        }
        else
        {
            return await Task.FromResult((Users)null);
        }
    }

    public async Task<Users> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return await db.Users
                       .AsAsyncEnumerable()
                       .SingleOrDefault(p => p.Login.Equals(normalizedUserName, StringComparison.OrdinalIgnoreCase), cancellationToken);
    }

    public Task SetPasswordHashAsync(Users user, string passwordHash, CancellationToken cancellationToken)
    {
        user.Password = passwordHash;

        return Task.FromResult((object)null);
    }

    public Task<string> GetPasswordHashAsync(Users user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Password);
    }

    public Task<bool> HasPasswordAsync(Users user, CancellationToken cancellationToken)
    {
        return Task.FromResult(!string.IsNullOrWhiteSpace(user.Password));
    }
}
}
