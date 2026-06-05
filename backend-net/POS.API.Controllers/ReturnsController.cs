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
public class ReturnsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReturnsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReturnDTO>>> GetReturns()
    {
        var returns = await _context.Returns
            .Include(r => r.Sale)
            .Include(r => r.User)
            .Include(r => r.Items).ThenInclude(i => i.Product)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ReturnDTO
            {
                Id = r.Id,
                SaleId = r.SaleId,
                Reason = r.Reason,
                Total = r.Total,
                UserName = r.User != null ? r.User.FullName : null,
                CreatedAt = r.CreatedAt,
                Items = r.Items.Select(i => new ReturnItemDTO
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product != null ? i.Product.Name : null,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            })
            .ToListAsync();

        return Ok(returns);
    }

    [HttpPost]
    public async Task<ActionResult<ReturnDTO>> CreateReturn([FromBody] CreateReturnRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        int userId = 0;
        if (userIdClaim != null) int.TryParse(userIdClaim.Value, out userId);

        var entity = new POS.API.Models.Return
        {
            SaleId = request.SaleId,
            UserId = userId > 0 ? userId : null,
            Reason = request.Reason,
            Total = request.Items.Sum(i => i.TotalPrice),
            CreatedAt = System.DateTime.UtcNow
        };
        _context.Returns.Add(entity);
        await _context.SaveChangesAsync();

        foreach (var item in request.Items)
        {
            var returnItem = new POS.API.Models.ReturnItem
            {
                ReturnId = entity.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice
            };
            _context.ReturnItems.Add(returnItem);

            var product = await _context.Products.FindAsync(item.ProductId);
            if (product != null)
                product.Stock += item.Quantity;
        }
        await _context.SaveChangesAsync();

        return Ok(new ReturnDTO
        {
            Id = entity.Id,
            SaleId = entity.SaleId,
            Reason = entity.Reason,
            Total = entity.Total,
            CreatedAt = entity.CreatedAt
        });
    }
}
