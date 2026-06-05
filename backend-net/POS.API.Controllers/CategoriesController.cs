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
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("product-count")]
    public async Task<ActionResult<List<CategoryProductCountDTO>>> GetCategoryProductCount()
    {
        var result = await _context.Categories
            .Select(c => new CategoryProductCountDTO
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
                ProductCount = c.Products.Count
            })
            .ToListAsync();

        return Ok(result);
    }
}
