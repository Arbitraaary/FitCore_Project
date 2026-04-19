using FitCore_API.DTOs;

namespace FitCore_API.Abstractions.Services;

public interface IUserService
{
    public Task<Guid> RegisterManagerAsync(ManagerRegistrationDto dto, CancellationToken ct);
    public Task<Guid> RegisterCoachAsync(CoachRegistrationDto dto, CancellationToken ct);
    public Task<Guid> RegisterClientAsync(ClientRegistrationDto dto, CancellationToken ct);
}