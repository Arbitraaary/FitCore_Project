using FitCore_API.DTOs;

namespace FitCoreAPI.Abstractions.Services;

public interface IAuthService
{
    Task<UserResponseDto> VerifyUserAsync(LoginDto dto, CancellationToken ct);
}