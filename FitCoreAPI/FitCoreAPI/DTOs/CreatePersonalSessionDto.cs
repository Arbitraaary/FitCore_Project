namespace FitCoreAPI.DTOs;

public record CreatePersonalSessionDto(           
    Guid ClientId, 
    Guid CoachId,
    Guid RoomId,
    string Name,
    DateTime StartTime,             
    DateTime EndTime);