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

        return Ok(new UserDTO
        {
            Id = entity.Id,
            Username = entity.Username,
            FullName = entity.FullName,
            Email = entity.Email,
            RoleId = entity.RoleId,
            IsActive = entity.IsActive
        });
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
