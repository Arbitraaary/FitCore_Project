using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class LocationRepository: ILocationRepository
{
    private readonly FitCoreDbContext _dbContext;
    
    public LocationRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<LocationModel?> GetByIdAsync(Guid locationId, CancellationToken ct)
    {
        return await _dbContext.Locations.FindAsync([locationId], ct);
    }

    public async Task<List<LocationModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Locations.ToListAsync(ct);
    }

    public async Task AddAsync(LocationModel location, CancellationToken ct)
    {
       _dbContext.Locations.Add(location);
       await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(LocationModel location, CancellationToken ct)
    {
        _dbContext.Locations.Update(location);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid locationId, CancellationToken ct)
    {
        var location = await GetByIdAsync(locationId, ct);
        if (location != null)
        {
            _dbContext.Locations.Remove(location);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}