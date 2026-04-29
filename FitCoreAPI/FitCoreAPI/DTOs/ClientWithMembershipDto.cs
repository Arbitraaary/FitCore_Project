using FitCoreAPI.DTOs;

namespace FitCore_API.DTOs;

public record ClientWithMembershipDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    MembershipTypeDto? ActiveMembership
    );