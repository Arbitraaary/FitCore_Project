using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Repositories;

public interface IEquipmentRepository
{
    public Task<EquipmentModel?> GetByIdAsync(int equipmentId, CancellationToken ct);
    public Task<List<EquipmentModel>> GetAllAsync(CancellationToken ct);
    public Task<List<EquipmentModel>> GetByLocationIdAsync(string locationName, CancellationToken ct);
    public Task AddAsync(EquipmentModel equipment, CancellationToken ct);
    public Task UpdateAsync(EquipmentModel equipment, CancellationToken ct);
    public Task DeleteAsync(int equipmentId, CancellationToken ct);
}