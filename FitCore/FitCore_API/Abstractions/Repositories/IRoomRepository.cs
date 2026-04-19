using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Repositories;

public interface IRoomRepository
{
    public Task<RoomModel?> GetByIdAsync(Guid roomId, CancellationToken ct);
    public Task<List<RoomModel>> GetAllAsync(CancellationToken ct);
    public Task<List<RoomModel>> GetByLocationIdAsync(Guid locationId, CancellationToken ct);
    public Task AddAsync(RoomModel room, CancellationToken ct);
    public Task UpdateAsync(RoomModel room, CancellationToken ct);
    public Task DeleteAsync(Guid roomId, CancellationToken ct);
}