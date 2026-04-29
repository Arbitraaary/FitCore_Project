namespace FitCore_API.DTOs;

public record AssignMembershipDto(
    Guid ClientId,
    Guid MembershipTypeId
    );