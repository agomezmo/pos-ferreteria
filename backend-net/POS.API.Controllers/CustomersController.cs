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
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _context;

    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetCustomers()
    {
        var customers = await _context.Customers
            .OrderBy(c => c.FullName)
            .Select(c => new CustomerDTO
            {
                Id = c.Id,
                FullName = c.FullName,
                DocumentNumber = c.DocumentNumber,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                IsActive = c.IsActive
            })
            .ToListAsync();

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
    {
        var customer = await _context.Customers
            .Where(c => c.Id == id)
            .Select(c => new CustomerDTO
            {
                Id = c.Id,
                FullName = c.FullName,
                DocumentNumber = c.DocumentNumber,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                IsActive = c.IsActive
            })
            .FirstOrDefaultAsync();

        if (customer == null)
            return NotFound(new { error = "Cliente no encontrado" });
        return Ok(customer);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<CustomerDTO>>> SearchCustomers([FromQuery] string q)
    {
        if (string.IsNullOrEmpty(q))
            return await GetCustomers();

        var customers = await _context.Customers
            .Where(c => c.FullName.Contains(q) || c.DocumentNumber.Contains(q) || c.Phone.Contains(q))
            .OrderBy(c => c.FullName)
            .Select(c => new CustomerDTO
            {
                Id = c.Id,
                FullName = c.FullName,
                DocumentNumber = c.DocumentNumber,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                IsActive = c.IsActive
            })
            .ToListAsync();

        return Ok(customers);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] CreateCustomerRequest request)
    {
        var entity = new POS.API.Models.Customer
        {
            FullName = request.FullName,
            DocumentNumber = request.DocumentNumber,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            IsActive = true
        };
        _context.Customers.Add(entity);
        await _context.SaveChangesAsync();

        return Ok(new CustomerDTO
        {
            Id = entity.Id,
            FullName = entity.FullName,
            DocumentNumber = entity.DocumentNumber,
            Email = entity.Email,
            Phone = entity.Phone,
            Address = entity.Address,
            IsActive = entity.IsActive
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerDTO>> UpdateCustomer(int id, [FromBody] UpdateCustomerRequest request)
    {
        var entity = await _context.Customers.FindAsync(id);
        if (entity == null)
            return NotFound(new { error = "Cliente no encontrado" });

        if (request.FullName != null) entity.FullName = request.FullName;
        if (request.DocumentNumber != null) entity.DocumentNumber = request.DocumentNumber;
        if (request.Email != null) entity.Email = request.Email;
        if (request.Phone != null) entity.Phone = request.Phone;
        if (request.Address != null) entity.Address = request.Address;
        if (request.IsActive.HasValue) entity.IsActive = request.IsActive.Value;

        await _context.SaveChangesAsync();

        return Ok(new CustomerDTO
        {
            Id = entity.Id,
            FullName = entity.FullName,
            DocumentNumber = entity.DocumentNumber,
            Email = entity.Email,
            Phone = entity.Phone,
            Address = entity.Address,
            IsActive = entity.IsActive
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        var entity = await _context.Customers.FindAsync(id);
        if (entity == null)
            return NotFound(new { error = "Cliente no encontrado" });

        entity.IsActive = false;
        await _context.SaveChangesAsync();
        return Ok(new { message = "Cliente desactivado" });
    }
}
