using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using FitCoreAPI.Abstractions.Repositories;
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
        return await _dbContext.GroupTrainingSessions
            .Include(gts => gts.ClientGroupSessions)
            .Include(gts => gts.Coach)
            .ThenInclude(c => c.User)
            .FirstAsync(gts => gts.Id == sessionId, ct);
    }

    public async Task<List<GroupTrainingSessionModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessions
            .Include(gts => gts.ClientGroupSessions)
            .Include(gts => gts.Coach)
            .ThenInclude(c => c.User)
            .ToListAsync(ct);
    }

    public async Task<List<GroupTrainingSessionModel>> GetByClientIdAsync(Guid clientId, CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessions
            .Include(gts => gts.ClientGroupSessions)
            .Include(gts => gts.Coach)
            .ThenInclude(c => c.User)
            .Where(s => s.ClientGroupSessions.Any(cgs => cgs.ClientId == clientId))
            .AsNoTracking()
            .ToListAsync(ct);
    }
    public async Task<List<GroupTrainingSessionModel>> GetByCoachIdAsync(Guid coachId, CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessions
            .Include(gts => gts.ClientGroupSessions)
            .Include(gts => gts.Coach)
            .ThenInclude(c => c.User)
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
    
    public async Task<bool> IsFullAsync(Guid sessionId, CancellationToken ct)
    {
        var sessionData = await _dbContext.GroupTrainingSessions
            .Where(s => s.Id == sessionId)
            .Select(s => new 
            { 
                s.Capacity, 
                EnrolledCount = s.ClientGroupSessions.Count() 
            })
            .FirstOrDefaultAsync(ct);

        if (sessionData == null) return false;

        return sessionData.EnrolledCount >= sessionData.Capacity;
    }

    public async Task<List<GroupTrainingSessionModel>> GetByLocationAsync(string locationName, CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessions
            .Include(gts => gts.ClientGroupSessions)
            .Include(gts => gts.Coach)
            .ThenInclude(c => c.User)
            .Include(gts => gts.Room)
            .Where(gts => gts.Room.LocationName == locationName)
            .ToListAsync(ct);
    }

    public async Task<List<GroupTrainingSessionModel>> GetAllWithCoachAndRoom(CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessions
            .Include(s => s.ClientGroupSessions)
            .Include(gts => gts.Coach).ThenInclude(c => c.User).Include(gts => gts.Coach.Location)
            .Include(gts => gts.Room)
            .ToListAsync(ct);
    }

    public async Task<List<GroupTrainingSessionModel>> GetAllWithCoachAndRoomById(Guid id, CancellationToken ct)
    {
        return await _dbContext.GroupTrainingSessions
            .Include(s => s.ClientGroupSessions)
            .Include(gts => gts.Coach)
            .ThenInclude(c => c.User)
            .Include(gts => gts.Coach.Location)
            .Include(gts => gts.Room).Where(gts => gts.CoachId == id)
            .ToListAsync(ct);
        
    }
}