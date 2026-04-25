using FitCore_API.Entities.Auxiliary;

namespace FitCore_API.Abstractions.Repositories;

using FitCore_API.Entities;

public interface IClientRepository
{
    public Task<ClientModel?> GetByIdAsync(Guid userId, CancellationToken ct);
    public Task<List<ClientModel>> GetAllAsync(CancellationToken ct);
    public Task CreateAsync(ClientModel client, CancellationToken ct);
}