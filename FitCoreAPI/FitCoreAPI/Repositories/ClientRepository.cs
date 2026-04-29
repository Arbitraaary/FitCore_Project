using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using FitCore_API.Entities.Auxiliary;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class ClientRepository: IClientRepository
{
    private readonly FitCoreDbContext _dbContext;
    
    public ClientRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ClientModel?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        return await _dbContext.Clients.Include(c=>c.User).FirstOrDefaultAsync(c=>c.UserId == userId, ct);
    }

    public async Task<List<ClientModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Clients.Include(c => c.User).ToListAsync(ct);
    }

    public async Task CreateAsync(ClientModel client, CancellationToken ct)
    {
        await _dbContext.Clients.AddAsync(client, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<List<ClientModel>> GetWithMembershipsAsync()
    {
        return await _dbContext.Clients.Include(c => c.User).Include(c => c.ClientMemberships).ThenInclude(cm => cm.MembershipType).ToListAsync();
    }
}