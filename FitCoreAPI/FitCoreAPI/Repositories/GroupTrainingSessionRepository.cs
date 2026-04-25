using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class GroupTrainingSessionRepository : IGroupTrainingSessionRepository
{
    private readonly FitCoreDbContext _dbContext;

    public GroupTrainingSessionRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GroupTrainingSessionModel?> GetByIdAsync(Guid sessionId, CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessions.FindAsync([sessionId], ct);
    }

    public async Task<List<GroupTrainingSessionModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessions.ToListAsync(ct);
    }

    public async Task<List<GroupTrainingSessionModel>> GetByCoachIdAsync(Guid coachId, CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessions
            .Where(s => s.CoachId == coachId)
            .ToListAsync(ct);
    }

    public async Task<List<GroupTrainingSessionModel>> GetByRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessions
            .Where(s => s.RoomId == roomId)
            .ToListAsync(ct);
    }

    public async Task AddAsync(GroupTrainingSessionModel session, CancellationToken ct)
    {
        _dbContext.GroupTrainingSessions.Add(session);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(GroupTrainingSessionModel session, CancellationToken ct)
    {
        _dbContext.GroupTrainingSessions.Update(session);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid sessionId, CancellationToken ct)
    {
        var session = await GetByIdAsync(sessionId, ct);
        if (session != null)
        {
            _dbContext.GroupTrainingSessions.Remove(session);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}