using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.Services;

namespace POS.API.Controllers;

[ApiController]
[Route("api/whatsapp")]
[Authorize]
public class WhatsAppController : ControllerBase
{
    private readonly WhatsAppService _whatsAppService;

    public WhatsAppController(WhatsAppService whatsAppService)
    {
        _whatsAppService = whatsAppService;
    }

    [HttpGet("status")]
    public ActionResult GetStatus()
    {
        var status = _whatsAppService.GetStatus();
        return Ok(status);
    }

    [HttpGet("qr")]
    [Authorize(Roles = "admin")]
    public ActionResult GetQr()
    {
        var qr = _whatsAppService.GetQr();
        if (qr == null)
            return NotFound(new { error = "No hay código QR disponible. Verifica que WhatsApp esté conectando..." });

        return Ok(new { qr, code = qr });
    }

    [HttpPost("reconnect")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Reconnect()
    {
        try
        {
            await _whatsAppService.ReconnectAsync();
            return Ok(new { message = "Reconectando WhatsApp..." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
