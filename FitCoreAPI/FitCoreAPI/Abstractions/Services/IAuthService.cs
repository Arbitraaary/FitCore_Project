using FitCore_API.DTOs;

namespace FitCore_API.Abstractions.Services;

public interface IAuthService
{
    Task<UserResponseDto> VerifyUserAsync(LoginDto dto, CancellationToken ct);
}