using FitCore_API.Controllers;
using FitCore_API.DTOs;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitCoreAPI.Controllers;


[Route("[controller]/[action]")]
public class RoomController: BaseController
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomDto>>> GetRooms(CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var rooms = await _roomService.GetAllRoomsAsync(ct);
        return Ok(rooms);
    });

    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDto>> GetRoom(Guid id, CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var room = await _roomService.GetRoomById(id, ct);
        return Ok(room);
    });

}