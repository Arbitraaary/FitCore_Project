namespace FitCoreAPI.DTOs;

public record CreateGroupEnrollmentDto(
    Guid ClientId,
    Guid SessionId
    );