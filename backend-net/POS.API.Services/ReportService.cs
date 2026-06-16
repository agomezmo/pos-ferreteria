using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POS.API.DTOs;
using POS.API.Data;

namespace POS.API.Services;

public class ReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DailyReportDTO> GetDailyReportAsync(DateTime date)
    {
        var start = date.Date;
        var end = start.AddDays(1);

        var sales = await _context.Sales
            .Where(s => s.CreatedAt >= start && s.CreatedAt < end)
            .ToListAsync();

        var items = await _context.SaleItems
            .Include(si => si.Sale)
            .Where(si => si.Sale.CreatedAt >= start && si.Sale.CreatedAt < end)
            .ToListAsync();

        var expenses = await _context.Expenses
            .Where(e => e.CreatedAt >= start && e.CreatedAt < end)
            .SumAsync(e => (decimal?)e.Amount) ?? 0;

        var totalSales = sales.Count;
        var totalRevenue = sales.Sum(s => s.Total);
        var totalProductsSold = items.Sum(i => (int)i.Quantity);
        var averageTicket = totalSales > 0 ? totalRevenue / totalSales : 0;

        return new DailyReportDTO
        {
            Date = date,
            TotalSales = totalSales,
            TotalRevenue = totalRevenue,
            TotalCash = sales.Where(s => s.PaymentMethod == "Cash").Sum(s => s.Total),
            TotalCard = sales.Where(s => s.PaymentMethod == "Card").Sum(s => s.Total),
            TotalTransfer = sales.Where(s => s.PaymentMethod == "Transfer").Sum(s => s.Total),
            TotalCredit = sales.Where(s => s.PaymentMethod == "Credit").Sum(s => s.Total),
            TotalTax = sales.Sum(s => s.Tax),
            TotalDiscount = sales.Sum(s => s.Discount),
            TotalExpenses = expenses,
            TotalProductsSold = totalProductsSold,
            AverageTicket = averageTicket
        };
    }

    public async Task<List<TopProductDTO>> GetTopProductsAsync(DateTime startDate, DateTime endDate, int top = 10)
    {
        var end = endDate.Date.AddDays(1);
        return await _context.SaleItems
            .Include(si => si.Product).ThenInclude(p => p.Category)
            .Where(si => si.Sale.CreatedAt >= startDate && si.Sale.CreatedAt < end)
            .GroupBy(si => new { si.ProductId, si.Product.Name, si.Product.Code, CategoryName = si.Product.Category != null ? si.Product.Category.Name : "" })
            .Select(g => new TopProductDTO
            {
                ProductId = g.Key.ProductId,
                ProductName = g.Key.Name,
                ProductCode = g.Key.Code,
                CategoryName = g.Key.CategoryName,
                TotalQuantity = (int)g.Sum(si => si.Quantity),
                TotalRevenue = g.Sum(si => si.Subtotal)
            })
            .OrderByDescending(t => t.TotalQuantity)
            .Take(top)
            .ToListAsync();
    }

    public async Task<InventoryReportDTO> GetInventoryReportAsync()
    {
        var products = await _context.Products.ToListAsync();
        var lowStockItems = products
            .Where(p => p.Stock > 0 && p.Stock <= p.MinStock)
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Stock = p.Stock,
                MinStock = p.MinStock,
                SalePrice = p.SalePrice,
                IsActive = p.IsActive
            })
            .ToList();

        return new InventoryReportDTO
        {
            TotalProducts = products.Count,
            LowStockProducts = products.Count(p => p.Stock > 0 && p.Stock <= p.MinStock),
            OutOfStockProducts = products.Count(p => p.Stock <= 0),
            TotalInventoryValue = products.Sum(p => p.Stock * p.PurchasePrice),
            LowStockItems = lowStockItems
        };
    }
}
