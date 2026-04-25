using FitCore_API.Abstractions.Services;
using FitCore_API.DTOs;
using FitCore_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitCore_API.Controllers;

//[Authorize]
[Route("[controller]/[action]")]
[ApiController]
public class AuthController: BaseController
{
    private readonly AuthService _authService;
    private readonly UserService _userService;
    private readonly IJwtService _jwtService;
    public AuthController(AuthService authService, UserService userService, IJwtService jwtService)
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
        HttpContext.Response.Cookies.Append("AuthCookie", token, cookieOptions);
        return Ok(user);
    });
    
    [HttpPost]
    //[Authorize(Roles = "Manager, Admin")]
    public async Task<ActionResult> RegisterCoach(string email, CancellationToken ct) => await ExecuteSafely(async () =>
    { 
        //var userId = await _userService.RegisterCoachAsync(dto, ct);
        return Ok(email);
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