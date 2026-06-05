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
public class FacturasController : ControllerBase
{
    private readonly FacturaService _facturaService;

    public FacturasController(FacturaService facturaService)
    {
        _facturaService = facturaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FacturaDTO>>> GetFacturas()
    {
        var facturas = await _facturaService.GetFacturasAsync();
        return Ok(facturas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FacturaDTO>> GetFactura(int id)
    {
        var factura = await _facturaService.GetFacturaByIdAsync(id);
        if (factura == null)
            return NotFound(new { error = "Factura no encontrada" });
        return Ok(factura);
    }

    [HttpPost]
    public async Task<ActionResult<FacturaDTO>> CreateFactura([FromBody] FacturarRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);

        var result = await _facturaService.CreateFacturaAsync(request, userId);
        if (result == null)
            return BadRequest(new { error = "Venta no encontrada" });
        return Ok(result);
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> CancelFactura(int id)
    {
        var result = await _facturaService.CancelFacturaAsync(id);
        if (!result)
            return NotFound(new { error = "Factura no encontrada" });
        return Ok(new { message = "Factura cancelada" });
    }
}
