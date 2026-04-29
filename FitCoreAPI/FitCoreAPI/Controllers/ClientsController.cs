using FitCore_API.Abstractions.Services;
using FitCore_API.DTOs;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.Controllers;
using FitCoreAPI.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FitCore_API.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class ClientsController : BaseController
{
    private readonly IClientService _clientService;
    private readonly IMembershipTypeService _membershipTypeService;
    private readonly IPersonalTrainingSessionService _personalTrainingSessionService;
    private readonly IGroupTrainingSessionService _groupTrainingSessionService;
    
    public ClientsController(IClientService clientService, IMembershipTypeService membershipTypeService,
        IPersonalTrainingSessionService personalTrainingSessionService, IGroupTrainingSessionService groupTrainingSessionService)
    {
        _clientService = clientService;
        _membershipTypeService = membershipTypeService;
        _personalTrainingSessionService = personalTrainingSessionService;
        _groupTrainingSessionService = groupTrainingSessionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetClients(CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var clients = await _clientService.GetAllClients(ct);
        return Ok(clients);
    });

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetClient(Guid id, CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            var client = await _clientService.GetClientById(id, ct);
            return Ok(client);
        });

    [HttpPost]
    public async Task<IActionResult> AssignMembership([FromBody] AssignMembershipDto dto, CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            await _membershipTypeService.AssignMembershipAsync(dto.ClientId, dto.MembershipTypeId, ct);
            return Ok();
        });

    [HttpGet]
    public async Task<ActionResult<List<MembershipTypeDto>>> GetMemberships(Guid clientId, CancellationToken ct) =>
        await ExecuteSafely(async () =>
        {
            var memberships = await _membershipTypeService.GetClientMembershipsAsync(clientId, ct);
            return Ok(memberships);
        });
    
    [HttpGet]
    public async Task<ActionResult<List<PersonalSessionDto>>> GetPersonalSessions(Guid clientId, CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var personalSessions = await _personalTrainingSessionService.GetPersonalSessionsByClientIdAsync(clientId, ct);
        return Ok(personalSessions);
    });

    [HttpGet]
    public async Task<ActionResult<List<GroupSessionDto>>> GetGroupSessions(Guid clientId, CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var groupSessions = await _groupTrainingSessionService.GetGroupSessionsByClientIdAsync(clientId, ct);
        return Ok(groupSessions);
    });

    [HttpGet]
    public async Task<ActionResult<List<ClientWithMembershipDto>>> GetClientsAndActiveMembership(CancellationToken ct) => await ExecuteSafely(async () =>
    {
        var clients = await _clientService.GetClientsAndActiveMembership(ct);
        return Ok(clients);
    });
    
}