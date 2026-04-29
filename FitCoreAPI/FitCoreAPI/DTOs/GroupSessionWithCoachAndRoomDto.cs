namespace FitCoreAPI.DTOs;

public record GroupSessionWithCoachAndRoomDto(
    Guid Id,
    Guid CoachId,
    string CoachName,
    Guid RoomId,
    string Name,
    string Description,
    int Capacity,
    string Type,
    DateTime StartTime,
    DateTime EndTime ,
    List<Guid> EnrolledClientIds,
    CoachDto Coach,
    RoomDto Room
    );