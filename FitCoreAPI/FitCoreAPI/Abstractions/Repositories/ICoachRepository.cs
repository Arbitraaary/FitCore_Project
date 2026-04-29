using FitCore_API.Entities;

namespace FitCoreAPI.Abstractions.Repositories;

public interface ICoachRepository
{
    public Task<CoachModel?> GetByIdAsync(Guid userId, CancellationToken ct);
    public Task<List<CoachModel>> GetAllAsync(CancellationToken ct);
    public Task CreateAsync(CoachModel coach, CancellationToken ct);
    public Task UpdateAsync(CoachModel coach, CancellationToken ct);
    public Task<List<CoachModel>> GetAllByLocationAsync(string locationName, CancellationToken ct);
    Task<List<CoachModel>> GetAllByLocationWithSessionsAsync(string locationName, CancellationToken ct);
}