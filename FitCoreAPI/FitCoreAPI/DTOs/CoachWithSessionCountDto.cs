namespace FitCoreAPI.DTOs;

public record CoachWithSessionCountDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string UserType,
    string Specialization,
    LocationDto Location,
    int SessionCount
    );