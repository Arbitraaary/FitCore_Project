using FitCore_API.DTOs;
using FitCore_API.Entities;

namespace FitCore_API.Abstractions.Services;

public interface IJwtService
{
    string GenerateJwtToken(UserResponseDto user);
    CookieOptions GetCookieOptions();
}