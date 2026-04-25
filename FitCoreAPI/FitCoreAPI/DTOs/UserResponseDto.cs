namespace FitCore_API.DTOs;

public record UserResponseDto(
    Guid UserId,
    string UserType, 
    string Email,
    string PhoneNumber,
    string FirstName,
    string LastName
);