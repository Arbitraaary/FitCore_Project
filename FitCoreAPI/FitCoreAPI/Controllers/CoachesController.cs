using FitCore_API.Abstractions.Services;
using FitCore_API.Controllers;
using FitCore_API.DTOs;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitCoreAPI.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class CoachesController: BaseController
{
    private readonly ICoachService _coachService;
    private readonly IGroupTrainingSessionService _groupTrainingSessionService;
    private readonly IPersonalTrainingSessionService _personalTrainingSessionService;
    public CoachesController(ICoachService coachService, IGroupTrainingSessionService groupTrainingSessionService, IPersonalTrainingSessionService personalTrainingSessionService)
    {
        _coachService = coachService;
        _groupTrainingSessionService = groupTrainingSessionService;
        _personalTrainingSessionService = personalTrainingSessionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CoachDto>>> GetCoaches(CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var coaches = await _coachService.GetAllCoaches(ct);
        return Ok(coaches);
    });

    [HttpGet("{id}")]
    public async Task<ActionResult<CoachDto>> GetCoach(Guid id, CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var coach = await _coachService.GetCoachById(id, ct);
        return Ok(coach);
    });
    

    [HttpGet("{id}")]
    public async Task<ActionResult<List<PersonalSessionDto>>> GetPersonalSessions(Guid id, CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var personalSessions = await _personalTrainingSessionService.GetPersonalSessionsByCoachIdAsync(id, ct);
        return Ok(personalSessions);
    });

    [HttpGet("{id}")]
    public async Task<ActionResult<List<GroupSessionDto>>> GetGroupSessions(Guid id, CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var groupSessions = await _groupTrainingSessionService.GetGroupSessionsByCoachIdAsync(id, ct);
        return Ok(groupSessions);
    });
}