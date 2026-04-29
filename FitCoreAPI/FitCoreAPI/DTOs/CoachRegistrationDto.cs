using FitCore_API.Entities;

namespace FitCore_API.DTOs;

public record CoachRegistrationDto ( 
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Password,
    ESpecializationType Specialization,
    string LocationName
);