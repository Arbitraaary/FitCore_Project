namespace FitCoreAPI.DTOs;

public record RoomDto(
    Guid Id,
    string LocationName,
    string RoomType,
    int Capacity
);