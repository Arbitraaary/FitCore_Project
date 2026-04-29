using FitCore_API.DTOs;

namespace FitCoreAPI.Abstractions.Services;

public interface IJwtService
{
    string GenerateJwtToken(UserResponseDto user);
    CookieOptions GetCookieOptions();
}