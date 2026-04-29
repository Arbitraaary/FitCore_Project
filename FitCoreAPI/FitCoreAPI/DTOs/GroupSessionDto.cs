namespace FitCoreAPI.DTOs;

public record GroupSessionDto(
    Guid Id,
    Guid CoachId,
    Guid RoomId,
    string Name,
    string Description,
    int Capacity,
    string Type,
    DateTime StartTime,
    DateTime EndTime ,
    List<Guid> EnrolledClientIds
    );
