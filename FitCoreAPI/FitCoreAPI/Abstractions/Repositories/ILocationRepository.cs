using FitCore_API.Entities;

namespace FitCoreAPI.Abstractions.Repositories;

public interface ILocationRepository
{
    public Task<LocationModel?> GetByIdAsync(string locationName, CancellationToken ct);
    public Task<List<LocationModel>> GetAllAsync(CancellationToken ct);
    public Task AddAsync(LocationModel location, CancellationToken ct);
    public Task UpdateAsync(LocationModel location, CancellationToken ct);
    public Task DeleteAsync(string locationName, CancellationToken ct);
}