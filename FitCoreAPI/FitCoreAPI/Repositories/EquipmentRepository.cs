using FitCore_API.Abstractions.Repositories;
using FitCore_API.Context;
using FitCore_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitCore_API.Repositories;

public class EquipmentRepository : IEquipmentRepository
{
    private readonly FitCoreDbContext _dbContext;

    public EquipmentRepository(FitCoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EquipmentModel?> GetByIdAsync(int equipmentId, CancellationToken ct)
    {
        return await _dbContext.Equipments.FindAsync([equipmentId], ct);
    }

    public async Task<List<EquipmentModel>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Equipments.ToListAsync(ct);
    }

    public async Task<List<EquipmentModel>> GetByLocationIdAsync(Guid locationId, CancellationToken ct)
    {
        return await _dbContext.Equipments
            .Where(e => e.LocationId == locationId)
            .ToListAsync(ct);
    }

    public async Task AddAsync(EquipmentModel equipment, CancellationToken ct)
    {
        _dbContext.Equipments.Add(equipment);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(EquipmentModel equipment, CancellationToken ct)
    {
        _dbContext.Equipments.Update(equipment);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int equipmentId, CancellationToken ct)
    {
        var equipment = await GetByIdAsync(equipmentId, ct);
        if (equipment != null)
        {
            _dbContext.Equipments.Remove(equipment);
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}