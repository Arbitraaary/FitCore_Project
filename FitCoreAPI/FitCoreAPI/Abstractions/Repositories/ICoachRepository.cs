namespace FitCore_API.Abstractions.Repositories;

using FitCore_API.Entities;

public interface ICoachRepository
{
    public Task<CoachModel?> GetByIdAsync(Guid userId, CancellationToken ct);
    public Task<List<CoachModel>> GetAllAsync(CancellationToken ct);
    public Task CreateAsync(CoachModel coach, CancellationToken ct);
    public Task UpdateAsync(CoachModel coach, CancellationToken ct);
}