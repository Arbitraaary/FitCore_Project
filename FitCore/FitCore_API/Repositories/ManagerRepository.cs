using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class ManagerRepository: IManagerRepository
{
    private readonly FitCoreDbContext _dbContext;
    
    public ManagerRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ManagerModel?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        return await _dbContext.Managers.Include(m=>m.UserId).FirstOrDefaultAsync(m => m.UserId == userId, ct);
    }

    public async Task<List<ManagerModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Managers.Include(m=>m.UserId).ToListAsync(ct);
    }

    public async Task CreateAsync(ManagerModel manager, CancellationToken ct)
    {
       _dbContext.Managers.Add(manager);
       await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(ManagerModel manager, CancellationToken ct)
    {
        _dbContext.Managers.Update(manager);
        await _dbContext.SaveChangesAsync(ct);
    }
}