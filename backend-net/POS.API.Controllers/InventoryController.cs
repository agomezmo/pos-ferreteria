using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.API.DTOs;
using POS.API.Data;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InventoryController : ControllerBase
{
    private readonly AppDbContext _context;

    public InventoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("movements")]
    public async Task<ActionResult<List<InventoryMovementDTO>>> GetMovements([FromQuery] int? productId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var query = _context.InventoryMovements
            .Include(m => m.Product)
            .Include(m => m.User)
            .AsQueryable();

        if (productId.HasValue)
            query = query.Where(m => m.ProductId == productId.Value);
        if (startDate.HasValue)
            query = query.Where(m => m.CreatedAt >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(m => m.CreatedAt <= endDate.Value);

        var movements = await query
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new InventoryMovementDTO
            {
                Id = m.Id,
                ProductId = m.ProductId,
                ProductName = m.Product != null ? m.Product.Name : null,
                Type = m.Type,
                Quantity = m.Quantity,
                Notes = m.Notes,
                UserName = m.User != null ? m.User.FullName : null,
                CreatedAt = m.CreatedAt
            })
            .ToListAsync();

        return Ok(movements);
    }

    [HttpPost("movements")]
    public async Task<ActionResult<InventoryMovementDTO>> CreateMovement([FromBody] CreateInventoryMovementRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        int userId = 0;
        if (userIdClaim != null) int.TryParse(userIdClaim.Value, out userId);

        var product = await _context.Products.FindAsync(request.ProductId);
        if (product == null)
            return BadRequest(new { error = "Producto no encontrado" });

        var entity = new POS.API.Models.InventoryMovement
        {
            ProductId = request.ProductId,
            Type = request.Type,
            Quantity = request.Quantity,
            Notes = request.Notes,
            UserId = userId > 0 ? userId : null,
            CreatedAt = DateTime.UtcNow
        };
        _context.InventoryMovements.Add(entity);

        if (request.Type == "in")
            product.Stock += request.Quantity;
        else if (request.Type == "out")
            product.Stock -= request.Quantity;

        await _context.SaveChangesAsync();

        return Ok(new InventoryMovementDTO
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            Type = entity.Type,
            Quantity = entity.Quantity,
            Notes = entity.Notes,
            CreatedAt = entity.CreatedAt
        });
    }
}
