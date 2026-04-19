using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Repositories;

public interface IGroupTrainingSessionRepository
{
    public Task<GroupTrainingSessionModel?> GetByIdAsync(Guid sessionId, CancellationToken ct);
    public Task<List<GroupTrainingSessionModel>> GetAllAsync(CancellationToken ct);
    public Task<List<GroupTrainingSessionModel>> GetByCoachIdAsync(Guid coachId, CancellationToken ct);
    public Task<List<GroupTrainingSessionModel>> GetByRoomIdAsync(Guid roomId, CancellationToken ct);
    public Task AddAsync(GroupTrainingSessionModel session, CancellationToken ct);
    public Task UpdateAsync(GroupTrainingSessionModel session, CancellationToken ct);
    public Task DeleteAsync(Guid sessionId, CancellationToken ct);
}