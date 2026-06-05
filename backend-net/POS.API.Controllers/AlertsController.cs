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
public class AlertsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlertsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<AlertDTO>>> GetAlerts()
    {
        var alerts = await _context.Alerts
            .Include(a => a.User)
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new AlertDTO
            {
                Id = a.Id,
                Title = a.Title,
                Message = a.Message,
                Type = a.Type,
                IsRead = a.IsRead,
                CreatedAt = a.CreatedAt,
                UserName = a.User != null ? a.User.FullName : null
            })
            .ToListAsync();

        return Ok(alerts);
    }

    [HttpPost("{id}/read")]
    public async Task<ActionResult> MarkAsRead(int id)
    {
        var alert = await _context.Alerts.FindAsync(id);
        if (alert == null)
            return NotFound();

        alert.IsRead = true;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("read-all")]
    public async Task<ActionResult> MarkAllAsRead()
    {
        var unread = await _context.Alerts
            .Where(a => !a.IsRead)
            .ToListAsync();

        foreach (var alert in unread)
            alert.IsRead = true;

        await _context.SaveChangesAsync();
        return Ok();
    }
}
