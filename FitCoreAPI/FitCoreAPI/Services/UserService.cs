using FitCore_API.Abstractions.Repositories;
using FitCore_API.Abstractions.Services;
using FitCore_API.Entities;
using FitCore_API.Entities.Auxiliary; 
using FitCore_API.DTOs;

namespace FitCore_API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly ICoachRepository _coachRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IPasswordService _passwordService;

    public UserService(
        IUserRepository userRepository,
        IManagerRepository managerRepository,
        ICoachRepository coachRepository,
        IClientRepository clientRepository,
        IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _managerRepository = managerRepository;
        _coachRepository = coachRepository;
        _clientRepository = clientRepository;
        _passwordService = passwordService;
    }

    public async Task<Guid> RegisterManagerAsync(ManagerRegistrationDto dto, CancellationToken ct)
    {
        var baseUser = new BaseRegistrationDto(dto.FirstName, dto.LastName, dto.PhoneNumber, dto.Email, dto.Password);
        var user = await CreateBaseUserAsync(baseUser, EUserType.Manager, ct);

        var manager = new ManagerModel
        {
            UserId = user.Id,
            LocationId = dto.LocationId
        };

        await _managerRepository.CreateAsync(manager, ct);
        return user.Id;
    }

    public async Task<Guid> RegisterCoachAsync(CoachRegistrationDto dto, CancellationToken ct)
    {
        var baseUser = new BaseRegistrationDto(dto.FirstName, dto.LastName, dto.PhoneNumber, dto.Email, dto.Password);
        var user = await CreateBaseUserAsync(baseUser, EUserType.Coach, ct);

        var coach = new CoachModel
        {
            UserId = user.Id,
            Specialization = dto.Specialization,
            LocationId = dto.LocationId
        };

        await _coachRepository.CreateAsync(coach, ct);
        return user.Id;
    }

    public async Task<Guid> RegisterClientAsync(ClientRegistrationDto dto, CancellationToken ct)
    {
        var baseUser = new BaseRegistrationDto(dto.FirstName, dto.LastName, dto.PhoneNumber, dto.Email, dto.Password);
        var user = await CreateBaseUserAsync(baseUser, EUserType.Client, ct);

        var client = new ClientModel
        {
            UserId = user.Id
        };

        await _clientRepository.CreateAsync(client, ct);
        return user.Id;
    }

    private async Task<UserModel> CreateBaseUserAsync(BaseRegistrationDto dto, EUserType type, CancellationToken ct)
    {
        var existing = await _userRepository.GetByEmailAsync(dto.Email, ct);
        if (existing != null) throw new Exception("This email already exists");

        var (hash, salt) = _passwordService.HashPassword(dto.Password);

        var user = new UserModel
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            UserType = type,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        await _userRepository.CreateAsync(user, ct);
        return user;
    }

    public async Task<bool> GetByEmailAsync(string email, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(email, ct);
        return user != null;
    }
}