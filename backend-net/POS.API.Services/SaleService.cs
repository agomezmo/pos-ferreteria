using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POS.API.DTOs;
using POS.API.Data;
using POS.API.Models;

namespace POS.API.Services;

public class SaleService
{
    private readonly AppDbContext _context;

    public SaleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SaleDTO> CreateSaleAsync(CreateSaleRequest request, int userId)
    {
        var receiptNumber = await GenerateReceiptNumberAsync();

        var sale = new Sale
        {
            ReceiptNumber = receiptNumber,
            CustomerId = request.CustomerId,
            UserId = userId,
            CashRegisterSessionId = request.CashRegisterSessionId,
            Subtotal = request.Items.Sum(i => i.Quantity * i.UnitPrice),
            Discount = request.Discount,
            Tax = request.Tax,
            Total = request.Items.Sum(i => i.Quantity * i.UnitPrice) - request.Discount + request.Tax,
            PaymentMethod = request.PaymentMethod,
            PaymentStatus = "Completed",
            SaleType = "Cash",
            Status = "Completed",
            CreatedAt = DateTime.UtcNow
        };
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        foreach (var item in request.Items)
        {
            var saleItem = new SaleItem
            {
                SaleId = sale.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Subtotal = item.Quantity * item.UnitPrice
            };
            _context.SaleItems.Add(saleItem);

            var product = await _context.Products.FindAsync(item.ProductId);
            if (product != null)
                product.Stock -= item.Quantity;
        }
        await _context.SaveChangesAsync();

        if (request.Payments != null)
        {
            foreach (var p in request.Payments)
            {
                _context.Payments.Add(new Payment
                {
                    SaleId = sale.Id,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    Reference = p.Reference,
                    CreatedAt = DateTime.UtcNow
                });
            }
            await _context.SaveChangesAsync();
        }

        return (await GetSaleByIdAsync(sale.Id))!;
    }

    public async Task<SaleDTO?> GetSaleByIdAsync(int id)
    {
        return await _context.Sales
            .Include(s => s.Customer)
            .Include(s => s.User)
            .Include(s => s.SaleItems).ThenInclude(si => si.Product)
            .Include(s => s.Payments)
            .Where(s => s.Id == id)
            .Select(s => new SaleDTO
            {
                Id = s.Id,
                ReceiptNumber = s.ReceiptNumber,
                CustomerId = s.CustomerId,
                CustomerName = s.Customer != null ? s.Customer.FullName : null,
                UserName = s.User != null ? s.User.FullName : "",
                Subtotal = s.Subtotal,
                Discount = s.Discount,
                Tax = s.Tax,
                Total = s.Total,
                PaymentMethod = s.PaymentMethod,
                PaymentStatus = s.Status,
                CreatedAt = s.CreatedAt,
                Items = s.SaleItems.Select(si => new SaleItemDTO
                {
                    Id = si.Id,
                    ProductId = si.ProductId,
                    ProductName = si.Product != null ? si.Product.Name : null,
                    Quantity = si.Quantity,
                    UnitPrice = si.UnitPrice,
                    Subtotal = si.Subtotal
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<SaleListDTO>> GetSalesAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Sales
            .Include(s => s.Customer)
            .Include(s => s.User)
            .AsQueryable();

        if (startDate.HasValue)
            query = query.Where(s => s.CreatedAt >= startDate.Value);
        if (endDate.HasValue)
            query = query.Where(s => s.CreatedAt <= endDate.Value.AddDays(1));

        return await query
            .OrderByDescending(s => s.CreatedAt)
            .Select(s => new SaleListDTO
            {
                Id = s.Id,
                ReceiptNumber = s.ReceiptNumber,
                CustomerName = s.Customer != null ? s.Customer.FullName : null,
                UserName = s.User != null ? s.User.FullName : "",
                Total = s.Total,
                PaymentMethod = s.PaymentMethod,
                SaleType = s.SaleType ?? "Cash",
                CreatedAt = s.CreatedAt
            })
            .ToListAsync();
    }

    private async Task<string> GenerateReceiptNumberAsync()
    {
        var today = DateTime.UtcNow.ToString("yyyyMMdd");
        var count = await _context.Sales
            .CountAsync(s => s.CreatedAt.Date == DateTime.UtcNow.Date);

        return $"R-{today}-{count + 1:D4}";
    }
}
