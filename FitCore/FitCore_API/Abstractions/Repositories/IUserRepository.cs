using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Repositories;

public interface IUserRepository
{
    public Task<UserModel?> GetByEmailAsync(string email, CancellationToken ct);
    public Task<UserModel?> GetByIdAsync(Guid id, CancellationToken ct);
    public Task<List<UserModel>> GetAllAsync(CancellationToken ct);
    
    public Task CreateAsync(UserModel user, CancellationToken ct);
    public Task UpdateAsync(UserModel user, CancellationToken ct);
    public Task DeleteAsync(Guid id, CancellationToken ct);
}