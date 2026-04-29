using FitCore_API.Context;
using FitCoreAPI.Abstractions.Repositories;
using FitCoreAPI.Entities.Auxiliary;
using Microsoft.EntityFrameworkCore;

namespace FitCoreAPI.Repositories;

public class GroupTrainingSessionClientRepository : IGroupTrainingSessionClientRepository
{
    private readonly FitCoreDbContext _dbContext;

    public GroupTrainingSessionClientRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<GroupTrainingSessionClient>> GetBySessionIdAsync(Guid sessionId, CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessionClients.Where(gtsc => gtsc.GroupTrainingSessionId == sessionId).ToListAsync(ct);
    }
    
    public async Task<List<GroupTrainingSessionClient>> GetByClientIdAsync(Guid clientId, CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessionClients.Where(gtsc => gtsc.ClientId == clientId).ToListAsync(ct);
    }

    public async Task<List<GroupTrainingSessionClient>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessionClients.ToListAsync(ct);
    }

    public async Task AddAsync(GroupTrainingSessionClient session, CancellationToken ct)
    {
        await _dbContext.GroupTrainingSessionClients.AddAsync(session, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(GroupTrainingSessionClient session, CancellationToken ct)
    {
        _dbContext.GroupTrainingSessionClients.Update(session);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteBySessionAsync(Guid sessionId, CancellationToken ct)
    {
        _dbContext.Remove(_dbContext.GroupTrainingSessionClients.Where(gtsc => gtsc.GroupTrainingSessionId == sessionId));
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteByClientAsync(Guid clientId, CancellationToken ct)
    {
        _dbContext.Remove(_dbContext.GroupTrainingSessionClients.Where(gtsc => gtsc.ClientId == clientId));
        await _dbContext.SaveChangesAsync(ct);
    }
}