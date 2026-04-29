namespace FitCore_API.DTOs;

public record ClientDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
    );