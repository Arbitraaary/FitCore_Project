using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities.Auxiliary;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class ClientMembershipRepository: IClientMembershipRepository
{
    private readonly FitCoreDbContext _dbContext;

    public ClientMembershipRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ClientMembership?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Set<ClientMembership>()
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }

    public async Task<List<ClientMembership>> GetByClientIdAsync(Guid clientId, CancellationToken ct)
    {
        return await _dbContext.Set<ClientMembership>()
            .Where(m => m.ClientId == clientId)
            .ToListAsync(ct);
    }

    public async Task AddAsync(ClientMembership membership, CancellationToken ct)
    {
        await _dbContext.Set<ClientMembership>().AddAsync(membership, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(ClientMembership membership, CancellationToken ct)
    {
        _dbContext.Set<ClientMembership>().Update(membership);
        await _dbContext.SaveChangesAsync(ct);
    }
}