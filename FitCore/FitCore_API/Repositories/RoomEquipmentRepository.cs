using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class RoomEquipmentRepository : IRoomEquipmentRepository
{
    private readonly FitCoreDbContext _dbContext;

    public RoomEquipmentRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RoomEquipmentModel?> GetByIdAsync(Guid roomEquipmentId, CancellationToken ct)
    {
        return await _dbContext.RoomEquipments.FindAsync([roomEquipmentId], ct);
    }

    public async Task<List<RoomEquipmentModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.RoomEquipments.ToListAsync(ct);
    }

    public async Task<List<RoomEquipmentModel>> GetByRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        return await _dbContext.RoomEquipments
            .Where(re => re.RoomId == roomId)
            .ToListAsync(ct);
    }

    public async Task<List<RoomEquipmentModel>> GetByLocationIdAsync(Guid locationId, CancellationToken ct)
    {
        return await _dbContext.RoomEquipments
            .Where(re => re.LocationId == locationId)
            .ToListAsync(ct);
    }

    public async Task AddAsync(RoomEquipmentModel roomEquipment, CancellationToken ct)
    {
        _dbContext.RoomEquipments.Add(roomEquipment);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(RoomEquipmentModel roomEquipment, CancellationToken ct)
    {
        _dbContext.RoomEquipments.Update(roomEquipment);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid roomEquipmentId, CancellationToken ct)
    {
        var roomEquipment = await GetByIdAsync(roomEquipmentId, ct);
        if (roomEquipment != null)
        {
            _dbContext.RoomEquipments.Remove(roomEquipment);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}