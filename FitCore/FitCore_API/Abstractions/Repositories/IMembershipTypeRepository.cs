using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Repositories;

public interface IMembershipTypeRepository
{
    public Task<MembershipTypeModel?> GetByIdAsync(Guid membershipTypeId, CancellationToken ct);
    public Task<List<MembershipTypeModel>> GetAllAsync(CancellationToken ct);
    public Task AddAsync(MembershipTypeModel membershipType, CancellationToken ct);
    public Task UpdateAsync(MembershipTypeModel membershipType, CancellationToken ct);
    public Task DeleteAsync(Guid membershipTypeId, CancellationToken ct);
}