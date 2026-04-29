namespace FitCoreAPI.DTOs;

public record MembershipTypeDto (   
    Guid Id,
    string Name,
    string Description,
    int Duration,
    float Price
    );