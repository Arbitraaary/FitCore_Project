using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Repositories;

public interface IAdminRepository
{
    public Task<AdminModel?> GetByIdAsync(Guid userId, CancellationToken ct);
    public Task<List<AdminModel>> GetAllAsync(CancellationToken ct);
    public Task CreateAsync(AdminModel admin, CancellationToken ct);
}