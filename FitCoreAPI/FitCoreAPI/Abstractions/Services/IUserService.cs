using FitCore_API.DTOs;
using FitCoreAPI.DTOs;

namespace FitCoreAPI.Abstractions.Services;

public interface IUserService
{
    public Task<Guid> RegisterManagerAsync(ManagerRegistrationDto dto, CancellationToken ct);
    public Task<Guid> RegisterCoachAsync(CoachRegistrationDto dto, CancellationToken ct);
    public Task<Guid> RegisterClientAsync(ClientRegistrationDto dto, CancellationToken ct);
    Task<ManagerResponseDto> GetManager(Guid id, CancellationToken ct);
    Task<bool> GetByEmailAsync(string email, CancellationToken ct);
}