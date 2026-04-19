using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class PersonalTrainingSessionRepository : IPersonalTrainingSessionRepository
{
    private readonly FitCoreDbContext _dbContext;

    public PersonalTrainingSessionRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PersonalTrainingSessionModel?> GetByIdAsync(Guid sessionId, CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions.FindAsync([sessionId], ct);
    }

    public async Task<List<PersonalTrainingSessionModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions.ToListAsync(ct);
    }

    public async Task<List<PersonalTrainingSessionModel>> GetByClientIdAsync(Guid clientId, CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions
            .Where(s => s.ClientId == clientId)
            .ToListAsync(ct);
    }

    public async Task<List<PersonalTrainingSessionModel>> GetByCoachIdAsync(Guid coachId, CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions
            .Where(s => s.CoachId == coachId)
            .ToListAsync(ct);
    }

    public async Task<List<PersonalTrainingSessionModel>> GetByRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        return await _dbContext.PersonalTrainingSessions
            .Where(s => s.RoomId == roomId)
            .ToListAsync(ct);
    }

    public async Task AddAsync(PersonalTrainingSessionModel session, CancellationToken ct)
    {
        _dbContext.PersonalTrainingSessions.Add(session);
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
}