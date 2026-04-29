using FitCore_API.Controllers;
using FitCoreAPI.Abstractions.Services;
using FitCoreAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FitCoreAPI.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class MembershipTypeController: BaseController
{
    private readonly IMembershipTypeService _membershipTypeService;
    
    public MembershipTypeController(IMembershipTypeService membershipTypeService)
    {
        _membershipTypeService = membershipTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MembershipTypeDto>>> GetMembershipTypes(CancellationToken ct) =>
        await ExecuteSafely(async () =>
    {
        var memberships = await _membershipTypeService.GetMembershipTypesAsync(ct);
        return Ok(memberships);
    });
}