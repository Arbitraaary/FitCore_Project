using FitCore_API.Abstractions.Services;
using FitCore_API.DTOs;
using FitCoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitCoreAPI.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class PersonalTrainingSessionController: BaseController
{
    private readonly IPersonalTrainingSessionService _personalTrainingSessionService;

    public PersonalTrainingSessionController(IPersonalTrainingSessionService personalTrainingSessionService)
    {
        _personalTrainingSessionService = personalTrainingSessionService;
    }

    [HttpPost]
    public async Task<ActionResult> CreatePersonalSession([FromBody]CreatePersonalSessionDto dto ,CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            await _personalTrainingSessionService.CreatePersonalSession(dto, ct);
            return Ok();
        });

        
    [HttpGet("{id}")]
    public async Task<ActionResult<List<GroupSessionWithCoachAndRoomDto>>> GetAllPersonalSessionsWithCoachAndRoomById(Guid id, CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            var groupSessions = await _personalTrainingSessionService.GetAllPersonalSessionWithCoachAndRoomById(id, ct);
            return Ok(groupSessions);
        });
    

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonalSessionDto>> GetPersonalSession(Guid id, CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            var personalSession = await _personalTrainingSessionService.GetPersonalSessionById(id, ct);
            return  Ok(personalSession);
        });

}