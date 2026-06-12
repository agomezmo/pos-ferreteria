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
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetUsers()
    {
        var users = await _context.Users
            .Include(u => u.Role)
            .Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                Email = u.Email,
                RoleId = u.RoleId,
                RoleName = u.Role != null ? u.Role.Name : "",
                IsActive = u.IsActive,
                LastLogin = u.LastLogin,
                CreatedAt = u.CreatedAt
            })
            .OrderBy(u => u.FullName)
            .ToListAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .Where(u => u.Id == id)
            .Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                Email = u.Email,
                RoleId = u.RoleId,
                RoleName = u.Role != null ? u.Role.Name : "",
                IsActive = u.IsActive,
                LastLogin = u.LastLogin,
                CreatedAt = u.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (user == null)
            return NotFound(new { error = "Usuario no encontrado" });
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDTO>> UpdateUser(int id, [FromBody] UpdateUserRequest request)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity == null)
            return NotFound(new { error = "Usuario no encontrado" });

        if (request.FullName != null) entity.FullName = request.FullName;
        if (request.Email != null) entity.Email = request.Email;
        if (request.RoleId.HasValue) entity.RoleId = request.RoleId.Value;
        if (request.IsActive.HasValue) entity.IsActive = request.IsActive.Value;
        await _context.SaveChangesAsync();

        var updated = await _context.Users
            .Include(u => u.Role)
            .Where(u => u.Id == id)
            .Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                Email = u.Email,
                RoleId = u.RoleId,
                RoleName = u.Role != null ? u.Role.Name : "",
                IsActive = u.IsActive,
                LastLogin = u.LastLogin,
                CreatedAt = u.CreatedAt
            })
            .FirstOrDefaultAsync();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity == null)
            return NotFound(new { error = "Usuario no encontrado" });

        entity.IsActive = false;
        await _context.SaveChangesAsync();
        return Ok(new { message = "Usuario desactivado" });
    }
}
