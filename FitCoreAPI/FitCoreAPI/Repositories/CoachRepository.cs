using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using FitCoreAPI.Abstractions.Repositories;
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
        return await _dbContext.Coaches.Include(c => c.User).Include(c => c.Location).FirstOrDefaultAsync(c => c.UserId == userId, ct);
    }

    public async Task<List<CoachModel>> GetAllAsync(CancellationToken ct)
    {
       return await  _dbContext.Coaches.Include(c => c.User).Include(c => c.Location).ToListAsync(ct);
    }

    public async Task<List<CoachModel>> GetAllByLocationIdAsync(string locationName, CancellationToken ct)
    {
        return await _dbContext.Coaches
            .Include(c => c.User) 
            .Where(c => c.LocationName == locationName) 
            .ToListAsync(ct);
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

    public async Task<List<CoachModel>> GetAllByLocationAsync(string locationName, CancellationToken ct)
    {
        return await _dbContext.Coaches
            .Include(c => c.User)
            .Include(c => c.Location)
            .Where(c => c.LocationName == locationName)
            .ToListAsync(ct);
    }

    public async Task<List<CoachModel>> GetAllByLocationWithSessionsAsync(string locationName, CancellationToken ct)
    {
        return await _dbContext.Coaches
            .Include(c => c.User)
            .Include(c=> c.GroupTrainingSessions)
            .Include(c => c.PersonalTrainingSessions)
            .Include(c => c.Location)
            .Where(c => c.LocationName == locationName)
            .ToListAsync(ct);
        
    }
}