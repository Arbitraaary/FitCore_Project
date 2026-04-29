namespace FitCoreAPI.DTOs;

public record ManagerResponseDto(
    Guid Id,
    string UserType, 
    string Email,
    string PhoneNumber,
    string FirstName,
    string LastName,
    LocationDto Location
    );