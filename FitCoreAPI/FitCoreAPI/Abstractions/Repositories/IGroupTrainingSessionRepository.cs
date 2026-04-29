using FitCore_API.Entities;

namespace FitCoreAPI.Abstractions.Repositories;

public interface IGroupTrainingSessionRepository
{
    public Task<GroupTrainingSessionModel?> GetByIdAsync(Guid sessionId, CancellationToken ct);
    public Task<List<GroupTrainingSessionModel>> GetAllAsync(CancellationToken ct);
    public Task<List<GroupTrainingSessionModel>> GetByClientIdAsync(Guid clientId, CancellationToken ct);
    public Task<List<GroupTrainingSessionModel>> GetByCoachIdAsync(Guid coachId, CancellationToken ct);
    public Task<List<GroupTrainingSessionModel>> GetByRoomIdAsync(Guid roomId, CancellationToken ct);
    public Task AddAsync(GroupTrainingSessionModel session, CancellationToken ct);
    public Task UpdateAsync(GroupTrainingSessionModel session, CancellationToken ct);
    public Task DeleteAsync(Guid sessionId, CancellationToken ct);
    public Task<bool> IsFullAsync(Guid sessionId, CancellationToken ct);
    Task<List<GroupTrainingSessionModel>> GetByLocationAsync(string locationName, CancellationToken ct);
}