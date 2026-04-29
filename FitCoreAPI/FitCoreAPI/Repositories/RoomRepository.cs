using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly FitCoreDbContext _dbContext;

    public RoomRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RoomModel?> GetByIdAsync(Guid roomId, CancellationToken ct)
    {
        return await _dbContext.Rooms.FindAsync([roomId], ct);
    }

    public async Task<List<RoomModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Rooms.ToListAsync(ct);
    }

    public async Task<List<RoomModel>> GetByLocationIdAsync(string locationName, CancellationToken ct)
    {
        return await _dbContext.Rooms
            .Where(r => r.LocationName == locationName)
            .ToListAsync(ct);
    }

    public async Task AddAsync(RoomModel room, CancellationToken ct)
    {
        _dbContext.Rooms.Add(room);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(RoomModel room, CancellationToken ct)
    {
        _dbContext.Rooms.Update(room);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid roomId, CancellationToken ct)
    {
        var room = await GetByIdAsync(roomId, ct);
        if (room != null)
        {
            _dbContext.Rooms.Remove(room);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}