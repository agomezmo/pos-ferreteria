using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POS.API.DTOs;
using POS.API.Data;
using POS.API.Models;

namespace POS.API.Services;

public class ProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDTO>> GetProductsAsync(bool includeInactive = false)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .AsQueryable();

        if (!includeInactive)
            query = query.Where(p => p.IsActive);

        return await query
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                Code = p.Code,
                Barcode = p.Barcode,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : null,
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier != null ? p.Supplier.Name : null,
                PurchasePrice = p.PurchasePrice,
                SalePrice = p.SalePrice,
                WholesalePrice = p.WholesalePrice,
                Stock = p.Stock,
                MinStock = p.MinStock,
                Unit = p.Unit,
                IsActive = p.IsActive,
                RequiresTax = p.RequiresTax,
                IsService = p.IsService,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<ProductDTO?> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.Id == id)
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                Code = p.Code,
                Barcode = p.Barcode,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : null,
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier != null ? p.Supplier.Name : null,
                PurchasePrice = p.PurchasePrice,
                SalePrice = p.SalePrice,
                WholesalePrice = p.WholesalePrice,
                Stock = p.Stock,
                MinStock = p.MinStock,
                Unit = p.Unit,
                IsActive = p.IsActive,
                RequiresTax = p.RequiresTax,
                IsService = p.IsService,
                CreatedAt = p.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<List<ProductDTO>> SearchProductsAsync(string search)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.IsActive && (
                p.Code.Contains(search) ||
                p.Barcode.Contains(search) ||
                p.Name.Contains(search)))
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                Code = p.Code,
                Barcode = p.Barcode,
                Name = p.Name,
                CategoryName = p.Category != null ? p.Category.Name : null,
                Stock = p.Stock,
                SalePrice = p.SalePrice,
                IsActive = p.IsActive
            })
            .ToListAsync();
    }

    public async Task<ProductDTO> CreateProductAsync(CreateProductRequest request)
    {
        var entity = new Product
        {
            Code = request.Code,
            Barcode = request.Barcode,
            Name = request.Name,
            Description = request.Description,
            CategoryId = request.CategoryId,
            SupplierId = request.SupplierId,
            PurchasePrice = request.PurchasePrice,
            SalePrice = request.SalePrice,
            WholesalePrice = request.WholesalePrice,
            Stock = request.Stock,
            MinStock = request.MinStock,
            Unit = request.Unit ?? "pza",
            RequiresTax = request.RequiresTax,
            IsService = request.IsService,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();

        return await GetProductByIdAsync(entity.Id);
    }

    public async Task<ProductDTO?> UpdateProductAsync(int id, UpdateProductRequest request)
    {
        var entity = await _context.Products.FindAsync(id);
        if (entity == null) return null;

        if (request.Code != null) entity.Code = request.Code;
        if (request.Barcode != null) entity.Barcode = request.Barcode;
        if (request.Name != null) entity.Name = request.Name;
        if (request.Description != null) entity.Description = request.Description;
        if (request.CategoryId.HasValue) entity.CategoryId = request.CategoryId.Value;
        if (request.SupplierId.HasValue) entity.SupplierId = request.SupplierId.Value;
        if (request.PurchasePrice.HasValue) entity.PurchasePrice = request.PurchasePrice.Value;
        if (request.SalePrice.HasValue) entity.SalePrice = request.SalePrice.Value;
        if (request.WholesalePrice.HasValue) entity.WholesalePrice = request.WholesalePrice.Value;
        if (request.Stock.HasValue) entity.Stock = request.Stock.Value;
        if (request.MinStock.HasValue) entity.MinStock = request.MinStock.Value;
        if (request.Unit != null) entity.Unit = request.Unit;
        if (request.IsActive.HasValue) entity.IsActive = request.IsActive.Value;
        if (request.RequiresTax.HasValue) entity.RequiresTax = request.RequiresTax.Value;
        if (request.IsService.HasValue) entity.IsService = request.IsService.Value;

        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetProductByIdAsync(id);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var entity = await _context.Products.FindAsync(id);
        if (entity == null) return false;

        entity.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<CategoryDTO>> GetCategoriesAsync()
    {
        return await _context.Categories
            .Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryRequest request)
    {
        var entity = new Category { Name = request.Name };
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();

        return new CategoryDTO { Id = entity.Id, Name = entity.Name };
    }

    public async Task<List<SupplierDTO>> GetSuppliersAsync()
    {
        return await _context.Suppliers
            .Select(s => new SupplierDTO
            {
                Id = s.Id,
                Name = s.Name,
                ContactName = s.ContactName,
                Phone = s.Phone,
                Email = s.Email
            })
            .ToListAsync();
    }

    public async Task<SupplierDTO> CreateSupplierAsync(CreateSupplierRequest request)
    {
        var entity = new Supplier
        {
            Name = request.Name,
            ContactName = request.ContactName,
            Phone = request.Phone,
            Email = request.Email
        };
        _context.Suppliers.Add(entity);
        await _context.SaveChangesAsync();

        return new SupplierDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            ContactName = entity.ContactName,
            Phone = entity.Phone,
            Email = entity.Email
        };
    }
}
