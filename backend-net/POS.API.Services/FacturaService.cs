using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POS.API.DTOs;
using POS.API.Data;
using POS.API.Models;

namespace POS.API.Services;

public class FacturaService
{
    private readonly AppDbContext _context;

    public FacturaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<FacturaDTO>> GetFacturasAsync()
    {
        return await _context.Facturas
            .Include(f => f.Sale)
            .Include(f => f.CreatedByUser)
            .Select(f => new FacturaDTO
            {
                Id = f.Id,
                SaleId = f.SaleId,
                Folio = f.Folio,
                Uuid = f.Uuid ?? "",
                Serie = f.Serie ?? "",
                Subtotal = f.Subtotal,
                Iva = f.Iva,
                Total = f.Total,
                Status = f.Status,
                CreatedAt = f.CreatedAt,
                CustomerName = f.Customer != null ? f.Customer.FullName : null
            })
            .ToListAsync();
    }

    public async Task<FacturaDTO?> GetFacturaByIdAsync(int id)
    {
        return await _context.Facturas
            .Include(f => f.Sale)
            .Include(f => f.CreatedByUser)
            .Where(f => f.Id == id)
            .Select(f => new FacturaDTO
            {
                Id = f.Id,
                SaleId = f.SaleId,
                Folio = f.Folio,
                Uuid = f.Uuid ?? "",
                Serie = f.Serie ?? "",
                Subtotal = f.Subtotal,
                Iva = f.Iva,
                Total = f.Total,
                Status = f.Status,
                CreatedAt = f.CreatedAt,
                CustomerName = f.Customer != null ? f.Customer.FullName : null
            })
            .FirstOrDefaultAsync();
    }

    public async Task<FacturaDTO?> CreateFacturaAsync(FacturarRequest request, int userId)
    {
        var sale = await _context.Sales
            .Include(s => s.SaleItems)
            .Include(s => s.Customer)
            .FirstOrDefaultAsync(s => s.Id == request.SaleId);
        if (sale == null) return null;

        var entity = new Factura
        {
            SaleId = request.SaleId,
            CreatedByUserId = userId,
            CustomerId = sale.CustomerId,
            Folio = $"F{System.DateTime.UtcNow:yyyyMMddHHmmss}",
            Subtotal = sale.Subtotal,
            Iva = sale.Tax,
            Total = sale.Total,
            Status = "active",
            CreatedAt = System.DateTime.UtcNow
        };
        _context.Facturas.Add(entity);
        await _context.SaveChangesAsync();

        return await GetFacturaByIdAsync(entity.Id);
    }

    public async Task<bool> CancelFacturaAsync(int id)
    {
        var factura = await _context.Facturas.FindAsync(id);
        if (factura == null) return false;

        factura.Status = "cancelled";
        await _context.SaveChangesAsync();
        return true;
    }
}
