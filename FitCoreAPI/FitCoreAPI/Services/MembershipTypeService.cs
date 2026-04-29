using FitCore_API.Abstractions.Repositories;
using FitCore_API.Abstractions.Services;
using FitCore_API.DTOs;
using FitCore_API.Entities;
using FitCore_API.Entities.Auxiliary;
using FitCoreAPI.Abstractions.Repositories;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FitCore_API.Services;

public class MembershipTypeService: IMembershipTypeService
{
    private readonly IMembershipTypeRepository _membershipTypeRepository;
    private readonly IClientMembershipRepository _clientMembershipRepository;
    
    public MembershipTypeService(IMembershipTypeRepository membershipTypeRepository, IClientMembershipRepository clientMembershipRepository)
    {
        _membershipTypeRepository = membershipTypeRepository;
        _clientMembershipRepository = clientMembershipRepository;
    }  
    public async Task<List<MembershipTypeDto>> GetMembershipTypesAsync(CancellationToken ct)
    {
        var memberships = await _membershipTypeRepository.GetAllAsync(ct);
        return memberships.Select(membership => new MembershipTypeDto(
            membership.Id,
            membership.Name,
            membership.Description,
            membership.Duration,
            membership.Price)).ToList();
    }
    
    public async Task AssignMembershipAsync(Guid clientId, Guid membershipTypeId, CancellationToken ct)
    {
        var existingMemberships = await _clientMembershipRepository.GetByClientIdAsync(clientId, ct);
        var hasActive = existingMemberships.Any(m => m.Status == EMembershipStatus.Active && m.EndDate >= DateOnly.FromDateTime(DateTime.UtcNow));

        if (hasActive)
        {
            throw new InvalidOperationException("Client already has an active membership.");
        }
        var type = await _membershipTypeRepository.GetByIdAsync(membershipTypeId, ct) 
                   ?? throw new Exception("Membership type not found");

        var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
        
        var newMembership = new ClientMembership
        {
            Id = Guid.NewGuid(),
            ClientId = clientId,
            MembershipTypeId = membershipTypeId,
            StartDate = startDate,
            EndDate = startDate.AddDays(type.Duration), 
            Status = EMembershipStatus.Active 
        };

        await _clientMembershipRepository.AddAsync(newMembership, ct);
    }

    public async Task<List<ClientMembershipDto>> GetClientMembershipsAsync(Guid clientId, CancellationToken ct)
    {
        var memberships = await _clientMembershipRepository.GetByClientIdAsync(clientId, ct);

        return memberships.Select(m => new ClientMembershipDto(
            m.Id, 
            m.MembershipTypeId,
            m.StartDate,
            m.EndDate,
            m.Status.ToString().ToLower()
        )).ToList();
    }
}
