using FitCore_API.DTOs;
using FitCoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitCoreAPI.Abstractions.Services;

public interface IRoomService
{
    Task<List<RoomDto>> GetAllRoomsAsync(CancellationToken ct);
    Task<RoomDto?> GetRoomById(Guid id, CancellationToken ct);
}