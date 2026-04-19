using FitCore_API.Abstractions.Repositories;
using FitCore_API.Abstractions.Services;
using System.Security.Claims;
using System.Text;
using FitCore_API.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace FitCore_API.Services;

public class AuthService: IAuthService
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    private  readonly IPasswordService _passwordService;
    
    public AuthService(IConfiguration config, IUserRepository userRepository,  IPasswordService passwordService){
        _config = config;
        _userRepository = userRepository;
        _passwordService = passwordService;
    }
    
    public async Task<LoginResponseDto> LoginAsync(LoginDto dto, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email, ct);
        
        if (user == null || !_passwordService.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new Exception("User not found");
        }
        var token = GenerateJwtToken(user);

        return new LoginResponseDto(
            Token: token,
            UserType: user.UserType.ToString(),
            FullName: $"{user.FirstName} {user.LastName}",
            UserId: user.Id
        );
    }
    
    private string GenerateJwtToken(UserModel user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserType.ToString()), 
            new Claim("FullName", $"{user.FirstName} {user.LastName}")
        };

        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}