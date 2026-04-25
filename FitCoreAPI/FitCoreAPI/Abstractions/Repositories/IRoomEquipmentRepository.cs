using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Repositories;

public interface IRoomEquipmentRepository
{
    public Task<RoomEquipmentModel?> GetByIdAsync(Guid roomEquipmentId, CancellationToken ct);
    public Task<List<RoomEquipmentModel>> GetAllAsync(CancellationToken ct);
    public Task<List<RoomEquipmentModel>> GetByRoomIdAsync(Guid roomId, CancellationToken ct);
    public Task<List<RoomEquipmentModel>> GetByLocationIdAsync(Guid locationId, CancellationToken ct);
    public Task AddAsync(RoomEquipmentModel roomEquipment, CancellationToken ct);
    public Task UpdateAsync(RoomEquipmentModel roomEquipment, CancellationToken ct);
    public Task DeleteAsync(Guid roomEquipmentId, CancellationToken ct);
}