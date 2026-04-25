using FitCore_API.Entities;

namespace FitCore_API.DTOs;

public record CoachRegistrationDto ( 
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    ESpecializationType Specialization,
    Guid LocationId
);