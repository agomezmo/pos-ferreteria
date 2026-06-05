using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.DTOs;
using POS.API.Services;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CashRegisterController : ControllerBase
{
    private readonly CashRegisterService _cashRegisterService;

    public CashRegisterController(CashRegisterService cashRegisterService)
    {
        _cashRegisterService = cashRegisterService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CashRegisterDTO>>> GetCashRegisters()
    {
        var registers = await _cashRegisterService.GetCashRegistersAsync();
        return Ok(registers);
    }

    [HttpPost]
    public async Task<ActionResult<CashRegisterDTO>> CreateCashRegister([FromBody] CreateCashRegisterRequest request)
    {
        var result = await _cashRegisterService.CreateCashRegisterAsync(request);
        return Ok(result);
    }

    [HttpPost("sessions/open")]
    public async Task<ActionResult<CashRegisterSessionDTO>> OpenSession([FromBody] OpenSessionRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);

        var result = await _cashRegisterService.OpenSessionAsync(request, userId);
        if (result == null)
            return BadRequest(new { error = "Ya hay una sesión activa o la caja no existe" });
        return Ok(result);
    }

    [HttpPost("sessions/{id}/close")]
    public async Task<ActionResult<CashRegisterSessionDTO>> CloseSession(int id, [FromBody] CloseSessionRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);

        var result = await _cashRegisterService.CloseSessionAsync(id, request, userId);
        if (result == null)
            return NotFound(new { error = "Sesión no encontrada o ya cerrada" });
        return Ok(result);
    }

    [HttpGet("sessions/current/{cashRegisterId}")]
    public async Task<ActionResult<CashRegisterSessionDTO>> GetCurrentSession(int cashRegisterId)
    {
        var result = await _cashRegisterService.GetCurrentSessionAsync(cashRegisterId);
        if (result == null)
            return NotFound(new { error = "No hay sesión activa" });
        return Ok(result);
    }

    [HttpGet("sessions")]
    public async Task<ActionResult<List<CashRegisterSessionDTO>>> GetSessions([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var sessions = await _cashRegisterService.GetSessionsAsync(startDate, endDate);
        return Ok(sessions);
    }
}
