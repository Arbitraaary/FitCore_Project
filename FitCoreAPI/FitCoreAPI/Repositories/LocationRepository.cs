using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using FitCoreAPI.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class LocationRepository: ILocationRepository
{
    private readonly FitCoreDbContext _dbContext;
    
    public LocationRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<LocationModel?> GetByIdAsync(string locationName, CancellationToken ct)
    {
        return await _dbContext.Locations.FindAsync([locationName], ct);
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

    public async Task DeleteAsync(string locationName, CancellationToken ct)
    {
        var location = await GetByIdAsync(locationName, ct);
        if (location != null)
        {
            _dbContext.Locations.Remove(location);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}