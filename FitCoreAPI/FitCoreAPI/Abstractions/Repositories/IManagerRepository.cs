using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Repositories;

public interface IManagerRepository
{
    Task<ManagerModel?> GetByIdAsync(Guid userId, CancellationToken ct);
    Task<List<ManagerModel>> GetAllAsync(CancellationToken ct);
    Task CreateAsync(ManagerModel manager, CancellationToken ct);
    Task UpdateAsync(ManagerModel manager, CancellationToken ct);
}