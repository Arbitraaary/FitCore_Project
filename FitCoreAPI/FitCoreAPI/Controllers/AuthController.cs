using FitCore_API.DTOs;
using FitCoreAPI.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitCoreAPI.Controllers;

//[Authorize]
[Route("[controller]/[action]")]
[ApiController]
public class AuthController: BaseController
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    public AuthController(IAuthService authService, IUserService userService, IJwtService jwtService)
    {
        _authService = authService;
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost]
    public Task<ActionResult> Login(LoginDto loginRequest, CancellationToken ct) => ExecuteSafely(async () =>
    {
        var user = await _authService.VerifyUserAsync(loginRequest, ct);
        var token = _jwtService.GenerateJwtToken(user);
        var cookieOptions = _jwtService.GetCookieOptions();
        HttpContext.Response.Cookies.Append("auth_cookie", token, cookieOptions);
        return Ok(user);
    });
    
    [HttpPost]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<ActionResult> RegisterCoach([FromBody] CoachRegistrationDto dto, CancellationToken ct) => await ExecuteSafely(async () =>
    { 
        var userId = await _userService.RegisterCoachAsync(dto, ct);
        return Ok(userId);
    });
    
    [HttpPost]
    [Authorize(Roles = "Manager, Admin")]
    public async Task<ActionResult> RegisterClient(ClientRegistrationDto dto, CancellationToken ct) => await ExecuteSafely(async () =>
    { 
        var userId = await _userService.RegisterClientAsync(dto, ct);
        return Ok();
    });
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> RegisterManager(ManagerRegistrationDto dto, CancellationToken ct) => await ExecuteSafely(async () =>
    { 
        var userId = await _userService.RegisterManagerAsync(dto, ct);
        return Ok();
    });
    
    [HttpGet]
    [AllowAnonymous] 
    public async Task<ActionResult<bool>> CheckEmail(string email, CancellationToken ct)
    {
        var isTaken = await _userService.GetByEmailAsync(email, ct);
        return Ok(isTaken);
    }
}