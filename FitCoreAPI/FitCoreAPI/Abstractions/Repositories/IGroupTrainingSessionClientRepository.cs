using FitCore_API.Entities.Auxiliary;
using FitCoreAPI.Entities.Auxiliary;

namespace FitCoreAPI.Abstractions.Repositories;

public interface IGroupTrainingSessionClientRepository
{
    
    Task<List<GroupTrainingSessionClient>> GetBySessionIdAsync(Guid sessionId, CancellationToken ct);
    Task<List<GroupTrainingSessionClient>> GetByClientIdAsync(Guid clientId, CancellationToken ct);
    Task<List<GroupTrainingSessionClient>> GetAllAsync(CancellationToken ct);
    Task AddAsync(GroupTrainingSessionClient session, CancellationToken ct);
    Task UpdateAsync(GroupTrainingSessionClient session, CancellationToken ct);
    Task DeleteBySessionAsync(Guid sessionId, CancellationToken ct);
    Task DeleteByClientAsync(Guid clientId, CancellationToken ct);
}