using FitCore_API.Abstractions.Repositories;
using FitCore_API.Abstractions.Services;
using FitCore_API.DTOs;
using FitCore_API.Entities;
using FitCoreAPI.DTOs;

namespace FitCoreAPI.Services;

public class ClientService: IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<List<ClientDto>> GetAllClients(CancellationToken ct)
    {
        var clients = await _clientRepository.GetAllAsync(ct);
        return clients.Select(client => new ClientDto(
            client.User.Id,
            client.User.FirstName,
            client.User.LastName,
            client.User.Email,
            client.User.PhoneNumber
        )).ToList();
    }

    public async Task<ClientDto?> GetClientById(Guid id, CancellationToken ct)
    {
        var client = await _clientRepository.GetByIdAsync(id, ct);
        if(client == null) return null;
        return new ClientDto(
            client.User.Id,
            client.User.FirstName,
            client.User.LastName,
            client.User.Email,
            client.User.PhoneNumber
        );
    }

    public async Task<List<ClientWithMembershipDto>> GetClientsAndActiveMembership(CancellationToken ct)
    {
        var clients = await _clientRepository.GetWithMembershipsAsync();
        return clients.Select(client => new ClientWithMembershipDto(
            client.User.Id,
            client.User.FirstName,
            client.User.LastName,
            client.User.Email,
            client.User.PhoneNumber,
            ((Func<MembershipTypeDto?>)(() =>
            {
                var firstActiveType = client.ClientMemberships.FirstOrDefault(cm => cm.Status == EMembershipStatus.Active)?
                    .MembershipType;
                
                if(firstActiveType == null) return null;
                
                var activeMembershipDto = new MembershipTypeDto(
                    firstActiveType.Id,
                    firstActiveType.Name,
                    firstActiveType.Description,
                    firstActiveType.Duration,
                    firstActiveType.Price
                );
                return activeMembershipDto;
            }))()
        )).ToList();
    }
}