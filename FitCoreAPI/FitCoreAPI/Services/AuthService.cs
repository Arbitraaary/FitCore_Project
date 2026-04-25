using FitCore_API.Abstractions.Repositories;
using FitCore_API.Abstractions.Services;
using FitCore_API.DTOs;

namespace FitCore_API.Services;

public class AuthService: IAuthService
{
    private readonly IUserRepository _userRepository;
    private  readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;
    
    public AuthService(IUserRepository userRepository,  IPasswordService passwordService, IJwtService jwtService){
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }
    
    public async Task<UserResponseDto> VerifyUserAsync(LoginDto dto, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email, ct);
        
        if (user == null)
        {
            throw new NullReferenceException("User not found");
        }

        if (!_passwordService.VerifyPassword(dto.Password, user.PasswordHash!, user.PasswordSalt!))
        {
            throw new UnauthorizedAccessException("Wrong password");
        }

        return new UserResponseDto(
            UserId: user.Id,
            UserType: user.UserType.ToString(),
            Email: user.Email,
            PhoneNumber: user.PhoneNumber,
            FirstName: user.FirstName,
            LastName: user.LastName
        );
    }
    
}