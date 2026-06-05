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
public class ExpensesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ExpensesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseDTO>>> GetExpenses([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var query = _context.Expenses
            .Include(e => e.User)
            .AsQueryable();

        if (startDate.HasValue)
            query = query.Where(e => e.CreatedAt >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(e => e.CreatedAt <= endDate.Value);

        var expenses = await query
            .OrderByDescending(e => e.CreatedAt)
            .Select(e => new ExpenseDTO
            {
                Id = e.Id,
                Description = e.Description,
                Amount = e.Amount,
                Category = e.Category,
                UserName = e.User != null ? e.User.FullName : null,
                CreatedAt = e.CreatedAt
            })
            .ToListAsync();

        return Ok(expenses);
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseDTO>> CreateExpense([FromBody] CreateExpenseRequest request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        int userId = 0;
        if (userIdClaim != null) int.TryParse(userIdClaim.Value, out userId);

        var entity = new POS.API.Models.Expense
        {
            Description = request.Description,
            Amount = request.Amount,
            Category = request.Category,
            UserId = userId > 0 ? userId : null,
            CreatedAt = DateTime.UtcNow
        };
        _context.Expenses.Add(entity);
        await _context.SaveChangesAsync();

        return Ok(new ExpenseDTO
        {
            Id = entity.Id,
            Description = entity.Description,
            Amount = entity.Amount,
            Category = entity.Category,
            CreatedAt = entity.CreatedAt
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteExpense(int id)
    {
        var entity = await _context.Expenses.FindAsync(id);
        if (entity == null)
            return NotFound(new { error = "Gasto no encontrado" });

        _context.Expenses.Remove(entity);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Gasto eliminado" });
    }
}
