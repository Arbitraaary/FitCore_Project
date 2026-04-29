using FitCore_API.Entities;
using FitCoreAPI.DTOs;

namespace FitCore_API.Abstractions.Repositories;

public interface IPersonalTrainingSessionRepository
{
    public Task<PersonalTrainingSessionModel?> GetByIdAsync(Guid sessionId, CancellationToken ct);
    public Task<List<PersonalTrainingSessionModel>> GetAllAsync(CancellationToken ct);
    public Task<List<PersonalTrainingSessionModel>> GetByClientIdAsync(Guid clientId, CancellationToken ct);
    public Task<List<PersonalTrainingSessionModel>> GetByCoachIdAsync(Guid coachId, CancellationToken ct);
    public Task<List<PersonalTrainingSessionModel>> GetByRoomIdAsync(Guid roomId, CancellationToken ct);
    public Task AddAsync(PersonalTrainingSessionModel session, CancellationToken ct);
    public Task UpdateAsync(PersonalTrainingSessionModel session, CancellationToken ct);
    public Task DeleteAsync(Guid sessionId, CancellationToken ct);
    public Task<List<PersonalTrainingSessionModel>> GetByLocationAsync(string locationName, CancellationToken ct);
    public Task<List<PersonalTrainingSessionModel>> GetAllWithCoachAndRoomById(Guid id, CancellationToken ct);
}