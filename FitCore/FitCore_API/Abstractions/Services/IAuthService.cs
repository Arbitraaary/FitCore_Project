namespace FitCore_API.Abstractions.Services;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginDto dto, CancellationToken ct);
}