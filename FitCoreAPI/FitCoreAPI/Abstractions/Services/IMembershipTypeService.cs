
using FitCore_API.DTOs;
using FitCoreAPI.DTOs;

namespace FitCoreAPI.Abstractions.Services;

public interface IMembershipTypeService
{
    public Task<List<MembershipTypeDto>> GetMembershipTypesAsync(CancellationToken ct);
    public Task AssignMembershipAsync(Guid clientId, Guid membershipTypeId, CancellationToken ct);
    public Task<List<ClientMembershipDto>> GetClientMembershipsAsync(Guid clientId, CancellationToken ct);
}