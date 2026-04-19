using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Repositories;

public interface ILocationRepository
{
    public Task<LocationModel?> GetByIdAsync(Guid locationId, CancellationToken ct);
    public Task<List<LocationModel>> GetAllAsync(CancellationToken ct);
    public Task AddAsync(LocationModel location, CancellationToken ct);
    public Task UpdateAsync(LocationModel location, CancellationToken ct);
    public Task DeleteAsync(Guid locationId, CancellationToken ct);
}