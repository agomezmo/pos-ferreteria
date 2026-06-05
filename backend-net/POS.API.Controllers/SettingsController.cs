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
public class SettingsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SettingsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<SystemSettingDTO>>> GetSettings()
    {
        var settings = await _context.SystemSettings
            .Select(s => new SystemSettingDTO
            {
                Key = s.Key,
                Value = s.Value,
                Description = s.Description
            })
            .ToListAsync();

        return Ok(settings);
    }

    [HttpGet("{key}")]
    public async Task<ActionResult<SystemSettingDTO>> GetSetting(string key)
    {
        var setting = await _context.SystemSettings
            .Where(s => s.Key == key)
            .Select(s => new SystemSettingDTO
            {
                Key = s.Key,
                Value = s.Value,
                Description = s.Description
            })
            .FirstOrDefaultAsync();

        if (setting == null)
            return NotFound(new { error = "Configuración no encontrada" });
        return Ok(setting);
    }

    [HttpPut("{key}")]
    public async Task<ActionResult<SystemSettingDTO>> UpdateSetting(string key, [FromBody] UpdateSystemSettingRequest request)
    {
        var setting = await _context.SystemSettings
            .FirstOrDefaultAsync(s => s.Key == key);

        if (setting == null)
        {
            setting = new POS.API.Models.SystemSetting
            {
                Key = key,
                Value = request.Value,
                Description = request.Description
            };
            _context.SystemSettings.Add(setting);
        }
        else
        {
            setting.Value = request.Value;
            if (request.Description != null)
                setting.Description = request.Description;
        }

        await _context.SaveChangesAsync();

        return Ok(new SystemSettingDTO
        {
            Key = setting.Key,
            Value = setting.Value,
            Description = setting.Description
        });
    }
}
