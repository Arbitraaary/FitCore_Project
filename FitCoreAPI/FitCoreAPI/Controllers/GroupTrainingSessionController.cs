using FitCore_API.Abstractions.Services;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitCoreAPI.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class GroupTrainingSessionController: BaseController
{
    private readonly IGroupTrainingSessionService _groupTrainingSessionService;

    public GroupTrainingSessionController(IGroupTrainingSessionService groupTrainingSessionService)
    {
        _groupTrainingSessionService = groupTrainingSessionService;
    }
        
    [HttpGet]
    public async Task<ActionResult<List<GroupSessionDto>>> GetAllGroupSessions(CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            var groupSessions = await _groupTrainingSessionService.GetAllGroupSessions(ct);
            return Ok(groupSessions);
        });

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupSessionDto>> GetGroupSession(Guid id, CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            var groupSession = await _groupTrainingSessionService.GetGroupSessionById(id, ct);
            return Ok(groupSession);
        });
    
    [HttpGet("{id}")]
    public async Task<ActionResult> IsGroupFull(Guid id, CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var isFull = await _groupTrainingSessionService.IsGroupFullAsync(id, ct);
        return Ok(isFull);  
    });
}