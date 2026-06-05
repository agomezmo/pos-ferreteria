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
public class SalesController : ControllerBase
{
    private readonly SaleService _saleService;

    public SalesController(SaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SaleListDTO>>> GetSales([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var sales = await _saleService.GetSalesAsync(startDate, endDate);
        return Ok(sales);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SaleDTO>> GetSale(int id)
    {
        var sale = await _saleService.GetSaleByIdAsync(id);
        if (sale == null)
            return NotFound(new { error = "Venta no encontrada" });
        return Ok(sale);
    }

    [HttpPost]
    public async Task<ActionResult<SaleDTO>> CreateSale([FromBody] CreateSaleRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim.Value);

        var result = await _saleService.CreateSaleAsync(request, userId);
        return Ok(result);
    }
}
