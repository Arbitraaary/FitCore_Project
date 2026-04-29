namespace FitCore_API.DTOs;

public record ClientMembershipDto(
    Guid Id,                        
    Guid MembershipTypeId,           
    DateOnly StartDate,             
    DateOnly EndDate,              
    string Status);