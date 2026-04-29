using FitCore_API.Entities.Auxiliary;

namespace FitCore_API.Abstractions.Repositories;

public interface IClientMembershipRepository
{
    Task<ClientMembership?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<ClientMembership>> GetByClientIdAsync(Guid clientId, CancellationToken ct);
    Task AddAsync(ClientMembership membership, CancellationToken ct);
    Task UpdateAsync(ClientMembership membership, CancellationToken ct);
}

