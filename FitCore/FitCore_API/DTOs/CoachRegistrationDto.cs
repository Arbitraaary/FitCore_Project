using FitCore_API.Entities;

namespace FitCore_API.DTOs;

public class CoachRegistrationDto: BaseRegistrationDto
{
    public ESpecializationType Specialization { get; set; }
    // public Guid LocationId { get; set; }
}