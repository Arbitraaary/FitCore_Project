using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class UserRepository: IUserRepository
{
    private readonly FitCoreDbContext _dbContext;
    
    public UserRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<UserModel?> GetByEmailAsync(string email, CancellationToken ct)
    {
       return await _dbContext.Users.FindAsync([email], ct);
    }

    public async Task<UserModel?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Users.FindAsync([id], ct);
    }

    public async Task<List<UserModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Users.ToListAsync(ct);
    }

    public async Task CreateAsync(UserModel user, CancellationToken ct)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(UserModel user, CancellationToken ct)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var user = await GetByIdAsync(id, ct);
        if (user != null)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}