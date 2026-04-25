using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class MembershipTypeRepository : IMembershipTypeRepository
{
    private readonly FitCoreDbContext _dbContext;

    public MembershipTypeRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MembershipTypeModel?> GetByIdAsync(Guid membershipTypeId, CancellationToken ct)
    {
        return await _dbContext.MembershipTypes.FindAsync([membershipTypeId], ct);
    }

    public async Task<List<MembershipTypeModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.MembershipTypes.ToListAsync(ct);
    }

    public async Task AddAsync(MembershipTypeModel membershipType, CancellationToken ct)
    {
        _dbContext.MembershipTypes.Add(membershipType);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(MembershipTypeModel membershipType, CancellationToken ct)
    {
        _dbContext.MembershipTypes.Update(membershipType);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid membershipTypeId, CancellationToken ct)
    {
        var membershipType = await GetByIdAsync(membershipTypeId, ct);
        if (membershipType != null)
        {
            _dbContext.MembershipTypes.Remove(membershipType);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}