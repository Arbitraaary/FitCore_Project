using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class CoachRepository: ICoachRepository
{
    private readonly FitCoreDbContext _dbContext;
    
    public CoachRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CoachModel?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        return await _dbContext.Coaches.Include(c => c.User).FirstOrDefaultAsync(c => c.UserId == userId, ct);
    }

    public async Task<List<CoachModel>> GetAllAsync(CancellationToken ct)
    {
       return await  _dbContext.Coaches.Include(c => c.User).ToListAsync(ct);
    }

    public async Task CreateAsync(CoachModel coach, CancellationToken ct)
    {
        _dbContext.Coaches.Add(coach);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(CoachModel coach, CancellationToken ct)
    {
        _dbContext.Coaches.Update(coach);
        await _dbContext.SaveChangesAsync(ct);
    }
}