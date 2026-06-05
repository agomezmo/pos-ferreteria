using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.DTOs;
using POS.API.Services;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        if (result == null)
            return Unauthorized(new { error = "Credenciales inválidas" });
        return Ok(result);
    }

    [Authorize]
    [HttpGet("users")]
    public async Task<ActionResult<List<UserDTO>>> GetUsers()
    {
        var users = await _authService.GetUsersAsync();
        return Ok(users);
    }

    [Authorize]
    [HttpPost("users")]
    public async Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUserRequest request)
    {
        var result = await _authService.CreateUserAsync(request);
        if (result == null)
            return BadRequest(new { error = "El usuario ya existe" });
        return Ok(result);
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim.Value);
        var result = await _authService.ChangePasswordAsync(userId, request);
        if (!result)
            return BadRequest(new { error = "Contraseña actual incorrecta" });
        return Ok(new { message = "Contraseña cambiada exitosamente" });
    }

    [Authorize]
    [HttpGet("roles")]
    public async Task<ActionResult<List<RoleDTO>>> GetRoles()
    {
        var roles = await _authService.GetRolesAsync();
        return Ok(roles);
    }
}
