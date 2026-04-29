using FitCore_API.Abstractions.Repositories;
using FitCore_API.DTOs;
using FitCore_API.Entities;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitCore_API.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    
    public RoomService (IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<List<RoomDto>> GetAllRoomsAsync(CancellationToken ct)
    {
        var rooms = await _roomRepository.GetAllAsync(ct);
        return rooms.Select(room => new RoomDto(
            room.Id,
            room.LocationName,
            room.RoomType.ToString(),
            room.Capacity
        )).ToList();
    }

    public async Task<RoomDto?> GetRoomById(Guid id, CancellationToken ct)
    {
        var room = await _roomRepository.GetByIdAsync(id, ct);
        if (room == null) return null;
        return new RoomDto(
            room.Id,
            room.LocationName,
            room.RoomType.ToString(),
            room.Capacity
        );
    }
}