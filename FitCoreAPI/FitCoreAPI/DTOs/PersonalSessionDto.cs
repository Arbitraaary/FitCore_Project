namespace FitCore_API.DTOs;

public record PersonalSessionDto(
    Guid Id,                        
    Guid ClientId, 
    Guid CoachId,
    Guid RoomId,
    string Type,
    string Name,
    DateTime StartTime,             
    DateTime EndTime
);