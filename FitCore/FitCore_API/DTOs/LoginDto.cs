public record LoginDto(
    string Email, 
    string Password
);

public record LoginResponseDto(
    string Token, 
    string UserType, 
    string FullName, 
    Guid UserId
);