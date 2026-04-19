using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class OccupiedEquipmentRepository : IOccupiedEquipmentRepository
{
    private readonly FitCoreDbContext _dbContext;

    public OccupiedEquipmentRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OccupiedEquipmentModel?> GetByIdAsync(Guid occupiedEquipmentId, CancellationToken ct)
    {
        return await _dbContext.OccupiedEquipments.FindAsync([occupiedEquipmentId], ct);
    }

    public async Task<List<OccupiedEquipmentModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.OccupiedEquipments.ToListAsync(ct);
    }

    public async Task<List<OccupiedEquipmentModel>> GetBySessionIdAsync(Guid sessionId, CancellationToken ct)
    {
        return await _dbContext.OccupiedEquipments
            .Where(oe => oe.SessionId == sessionId)
            .ToListAsync(ct);
    }

    public async Task AddAsync(OccupiedEquipmentModel occupiedEquipment, CancellationToken ct)
    {
        _dbContext.OccupiedEquipments.Add(occupiedEquipment);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(OccupiedEquipmentModel occupiedEquipment, CancellationToken ct)
    {
        _dbContext.OccupiedEquipments.Update(occupiedEquipment);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid occupiedEquipmentId, CancellationToken ct)
    {
        var occupiedEquipment = await GetByIdAsync(occupiedEquipmentId, ct);
        if (occupiedEquipment != null)
        {
            _dbContext.OccupiedEquipments.Remove(occupiedEquipment);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}