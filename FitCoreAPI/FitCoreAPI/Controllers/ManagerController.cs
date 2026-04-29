using FitCore_API.Abstractions.Services;
using FitCore_API.Controllers;
using FitCore_API.DTOs;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitCoreAPI.Controllers;

[Authorize(Roles = "Manager")]
[Route("[controller]/[action]")]
[ApiController]
public class ManagerController : BaseController
{
    private readonly IUserService _userService;
    private readonly IPersonalTrainingSessionService _personalTrainingSessionService;
    private readonly IGroupTrainingSessionService _groupTrainingSessionService;
    private readonly ICoachService _coachService;

    public ManagerController(IUserService userService, IPersonalTrainingSessionService personalTrainingSessionService, IGroupTrainingSessionService groupTrainingSessionService, ICoachService coachService)
    {
        _userService = userService;
        _personalTrainingSessionService = personalTrainingSessionService;
        _groupTrainingSessionService = groupTrainingSessionService;
        _coachService = coachService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ManagerResponseDto>> GetManager(Guid id, CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var manager = await _userService.GetManager(id, ct);
        return Ok(manager);
    });
    
    [HttpGet("{id}")]
    public async Task<ActionResult<List<PersonalSessionDto>>> GetPersonalSessions(Guid id, CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            var manager = await _userService.GetManager(id, ct);
                
            var personalSession = await _personalTrainingSessionService.GetPersonalSessionByLocation(manager.Location.Name, ct);
            return  Ok(personalSession);
        });
    
    [HttpGet("{id}")]
    public async Task<ActionResult<List<GroupSessionDto>>> GetGroupSessions(Guid id, CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            var manager = await _userService.GetManager(id, ct);
                
            var personalSession = await _groupTrainingSessionService.GetGroupSessionByLocation(manager.Location.Name, ct);
            return  Ok(personalSession);
        });
    
    [HttpGet("{id}")]
    public async Task<ActionResult<List<CoachDto>>> GetMyCoaches(Guid id, CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            var manager = await _userService.GetManager(id, ct);
                
            var personalSession = await _coachService.GetAllCoachesByLocation(manager.Location.Name, ct);
            return  Ok(personalSession);
        });
    
    [HttpGet("{id}")]
    public async Task<ActionResult<List<CoachWithSessionCountDto>>> GetMyCoachesWithSessionCount(Guid id, CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            var manager = await _userService.GetManager(id, ct);
                
            var personalSession = await _coachService.GetAllCoachesByLocationWithSessionCount(manager.Location.Name, ct);
            return  Ok(personalSession);
        });
}