namespace FitCoreAPI.DTOs;

public record PersonalSessionWithCoachAndRoomDto(
    Guid Id,                        
    Guid ClientId, 
    Guid CoachId,
    string CoachName,
    Guid RoomId,
    string Type,
    string Name,
    DateTime StartTime,             
    DateTime EndTime,
    CoachDto Coach,
    RoomDto Room
    );