using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using FitCoreAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FitCoreAPI.Repositories;

public class PersonalTrainingSessionRepository : IPersonalTrainingSessionRepository
{
    private readonly FitCoreDbContext _dbContext;

    public PersonalTrainingSessionRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PersonalTrainingSessionModel?> GetByIdAsync(Guid sessionId, CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions
            .Include(pts => pts.Coach)
            .ThenInclude(c => c.User)
            .FirstAsync(pts => pts.Id == sessionId, ct);
    }

    public async Task<List<PersonalTrainingSessionModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions
            .Include(pts => pts.Coach)
            .ThenInclude(c => c.User)
            .ToListAsync(ct);
    }

    public async Task<List<PersonalTrainingSessionModel>> GetByClientIdAsync(Guid clientId, CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions
            .Include(pts => pts.Coach)
            .ThenInclude(c => c.User)
            .Where(s => s.ClientId == clientId)
            .ToListAsync(ct);
    }

    public async Task<List<PersonalTrainingSessionModel>> GetByCoachIdAsync(Guid coachId, CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions
            .Include(pts => pts.Coach)
            .ThenInclude(c => c.User)
            .Where(s => s.CoachId == coachId)
            .ToListAsync(ct);
    }

    public async Task<List<PersonalTrainingSessionModel>> GetByRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions
            .Include(pts => pts.Coach)
            .ThenInclude(c => c.User)
            .Where(s => s.RoomId == roomId)
            .ToListAsync(ct);
    }

    public async Task AddAsync(PersonalTrainingSessionModel session, CancellationToken ct)
    {
        await _dbContext.PersonalTrainingSessions.AddAsync(session, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(PersonalTrainingSessionModel session, CancellationToken ct)
    {
        _dbContext.PersonalTrainingSessions.Update(session);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid sessionId, CancellationToken ct)
    {
        var session = await GetByIdAsync(sessionId, ct);
        if (session != null)
        {
            _dbContext.PersonalTrainingSessions.Remove(session);
            await _dbContext.SaveChangesAsync(ct);
        }
    }

    public async Task<List<PersonalTrainingSessionModel>> GetByLocationAsync(string locationName, CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions
            .Include(pts => pts.Coach)
            .ThenInclude(c => c.User)
            .Include(pts => pts.Room)
            .Where(pts => pts.Room.LocationName == locationName).ToListAsync(ct);
    }


    public async Task<List<PersonalTrainingSessionModel>> GetAllWithCoachAndRoomById(Guid id, CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions
            .Include(gts => gts.Coach)
            .ThenInclude(c => c.User)
            .Include(gts => gts.Coach.Location)
            .Include(gts => gts.Room)
            .Where(gts => gts.CoachId == id)
            .ToListAsync(ct);
        
    }
}