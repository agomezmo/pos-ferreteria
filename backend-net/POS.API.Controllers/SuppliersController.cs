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
public class SuppliersController : ControllerBase
{
    private readonly AppDbContext _context;

    public SuppliersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SupplierDTO>> UpdateSupplier(int id, [FromBody] CreateSupplierRequest request)
    {
        var entity = await _context.Suppliers.FindAsync(id);
        if (entity == null)
            return NotFound(new { error = "Proveedor no encontrado" });

        if (request.Name != null) entity.Name = request.Name;
        if (request.ContactName != null) entity.ContactName = request.ContactName;
        if (request.Phone != null) entity.Phone = request.Phone;
        if (request.Email != null) entity.Email = request.Email;
        await _context.SaveChangesAsync();

        return Ok(new SupplierDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            ContactName = entity.ContactName,
            Phone = entity.Phone,
            Email = entity.Email
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSupplier(int id)
    {
        var entity = await _context.Suppliers.FindAsync(id);
        if (entity == null)
            return NotFound(new { error = "Proveedor no encontrado" });

        _context.Suppliers.Remove(entity);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Proveedor eliminado" });
    }
}
