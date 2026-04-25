using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Repositories;

public interface IOccupiedEquipmentRepository
{
    public Task<OccupiedEquipmentModel?> GetByIdAsync(Guid occupiedEquipmentId, CancellationToken ct);
    public Task<List<OccupiedEquipmentModel>> GetAllAsync(CancellationToken ct);
    public Task<List<OccupiedEquipmentModel>> GetBySessionIdAsync(Guid sessionId, CancellationToken ct);
    public Task AddAsync(OccupiedEquipmentModel occupiedEquipment, CancellationToken ct);
    public Task UpdateAsync(OccupiedEquipmentModel occupiedEquipment, CancellationToken ct);
    public Task DeleteAsync(Guid occupiedEquipmentId, CancellationToken ct);
}