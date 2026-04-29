using FitCore_API.DTOs;

namespace FitCore_API.Abstractions.Services;

public interface IClientService
{
    public Task<List<ClientDto>> GetAllClients(CancellationToken ct);
    public Task<ClientDto?> GetClientById(Guid id, CancellationToken ct);
    Task<List<ClientWithMembershipDto>> GetClientsAndActiveMembership(CancellationToken ct);
}