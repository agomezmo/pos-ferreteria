using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.API.Data;
using POS.API.DTOs;
using POS.API.Models;

namespace POS.API.Controllers;

[ApiController]
[Route("api/campaigns")]
[Authorize]
public class CampaignsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<CampaignsController> _logger;

    public CampaignsController(AppDbContext context, ILogger<CampaignsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<CampaignDto>>> GetCampaigns([FromQuery] string? status)
    {
        var query = _context.Set<PromoCampaign>()
            .Include(c => c.CreatedByUser)
            .Select(c => new CampaignDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Status = c.Status,
                OfferType = c.OfferType,
                OfferValue = c.OfferValue,
                MinExpiryDays = c.MinExpiryDays,
                MaxExpiryDays = c.MaxExpiryDays,
                Notes = c.Notes,
                CreatedByName = c.CreatedByUser != null ? c.CreatedByUser.FullName : null,
                CreatedAt = c.CreatedAt,
                SentAt = c.SentAt,
                ProductCount = c.Products.Count,
                CustomerCount = c.Customers.Count,
                SentCount = c.Logs.Count(l => l.Status == "sent"),
            });

        if (!string.IsNullOrEmpty(status))
            query = query.Where(c => c.Status == status);

        var campaigns = await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
        return Ok(campaigns);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CampaignDetailDto>> GetCampaign(int id)
    {
        var campaign = await _context.Set<PromoCampaign>()
            .Include(c => c.CreatedByUser)
            .Include(c => c.Products).ThenInclude(p => p.Product).ThenInclude(p => p.Category)
            .Include(c => c.Customers).ThenInclude(c => c.Customer)
            .Include(c => c.Logs).ThenInclude(l => l.Customer)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (campaign == null)
            return NotFound(new { error = "Campaña no encontrada" });

        var dto = new CampaignDetailDto
        {
            Id = campaign.Id,
            Name = campaign.Name,
            Description = campaign.Description,
            Status = campaign.Status,
            OfferType = campaign.OfferType,
            OfferValue = campaign.OfferValue,
            MinExpiryDays = campaign.MinExpiryDays,
            MaxExpiryDays = campaign.MaxExpiryDays,
            Notes = campaign.Notes,
            CreatedByName = campaign.CreatedByUser?.FullName,
            CreatedAt = campaign.CreatedAt,
            SentAt = campaign.SentAt,
            Products = campaign.Products.Select(p => new CampaignProductDto
            {
                Id = p.Id,
                CampaignId = p.CampaignId,
                ProductId = p.ProductId,
                ProductName = p.Product.Name,
                ProductCode = p.Product.Code,
                Barcode = p.Product.Barcode,
                CategoryName = p.Product.Category != null ? p.Product.Category.Name : null,
                OfferPrice = p.OfferPrice,
                OriginalPrice = p.OriginalPrice,
                ExpiryDate = p.ExpiryDate,
            }).ToList(),
            Customers = campaign.Customers.Select(c => new CampaignCustomerDto
            {
                Id = c.Id,
                CampaignId = c.CampaignId,
                CustomerId = c.CustomerId,
                CustomerName = c.Customer.FullName,
                ContactEmail = c.ContactEmail,
                ContactPhone = c.ContactPhone,
                DocumentNumber = c.Customer.DocumentNumber,
            }).ToList(),
            Logs = campaign.Logs.OrderByDescending(l => l.SentAt).Select(l => new CampaignLogDto
            {
                Id = l.Id,
                CampaignId = l.CampaignId,
                CustomerId = l.CustomerId,
                CustomerName = l.Customer != null ? l.Customer.FullName : null,
                Channel = l.Channel,
                Recipient = l.Recipient,
                Subject = l.Subject,
                Status = l.Status,
                ErrorMessage = l.ErrorMessage,
                SentAt = l.SentAt,
            }).ToList(),
        };

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<PromoCampaign>> CreateCampaign([FromBody] CreateCampaignRequest request)
    {
        if (string.IsNullOrEmpty(request.Name))
            return BadRequest(new { error = "El nombre de la campaña es requerido" });
        if (request.ProductIds == null || request.ProductIds.Count == 0)
            return BadRequest(new { error = "Debe seleccionar al menos un producto" });
        if (request.CustomerIds == null || request.CustomerIds.Count == 0)
            return BadRequest(new { error = "Debe seleccionar al menos un cliente" });

        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        int userId = 0;
        if (userIdClaim != null) int.TryParse(userIdClaim.Value, out userId);

        var products = await _context.Set<Product>()
            .Where(p => request.ProductIds.Contains(p.Id))
            .Select(p => new { p.Id, p.Name, p.SalePrice, p.PurchasePrice, p.ExpiryDate })
            .ToListAsync();

        if (products.Count == 0)
            return BadRequest(new { error = "No se encontraron productos válidos" });

        var offerType = request.OfferType ?? "cost_price";
        var campaignProducts = products.Select(p =>
        {
            decimal offerPrice;
            if (offerType == "cost_price")
                offerPrice = p.PurchasePrice;
            else if (offerType == "percentage")
                offerPrice = p.SalePrice * (1 - (request.OfferValue ?? 0) / 100);
            else
                offerPrice = request.OfferValue ?? p.PurchasePrice;

            return new PromoCampaignProduct
            {
                ProductId = p.Id,
                OriginalPrice = p.SalePrice,
                OfferPrice = Math.Round(offerPrice, 2),
                ExpiryDate = p.ExpiryDate,
            };
        }).ToList();

        var customers = await _context.Set<Customer>()
            .Where(c => request.CustomerIds.Contains(c.Id))
            .Select(c => new { c.Id, c.Email, c.Phone })
            .ToListAsync();

        var campaign = new PromoCampaign
        {
            Name = request.Name,
            Description = request.Description,
            Status = "draft",
            OfferType = offerType,
            OfferValue = request.OfferValue,
            MinExpiryDays = request.MinExpiryDays,
            MaxExpiryDays = request.MaxExpiryDays,
            Notes = request.Notes,
            CreatedBy = userId > 0 ? userId : null,
            CreatedAt = DateTime.UtcNow,
            Products = campaignProducts,
            Customers = customers.Select(c => new PromoCampaignCustomer
            {
                CustomerId = c.Id,
                ContactEmail = c.Email,
                ContactPhone = c.Phone,
            }).ToList(),
        };

        _context.Set<PromoCampaign>().Add(campaign);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCampaign), new { id = campaign.Id }, campaign);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> UpdateCampaign(int id, [FromBody] UpdateCampaignRequest request)
    {
        var campaign = await _context.Set<PromoCampaign>().FindAsync(id);
        if (campaign == null)
            return NotFound(new { error = "Campaña no encontrada" });

        if (request.Name != null) campaign.Name = request.Name;
        if (request.Description != null) campaign.Description = request.Description;
        if (request.Status != null) campaign.Status = request.Status;
        if (request.OfferType != null) campaign.OfferType = request.OfferType;
        if (request.OfferValue.HasValue) campaign.OfferValue = request.OfferValue;
        if (request.Notes != null) campaign.Notes = request.Notes;
        campaign.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Campaña actualizada" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> DeleteCampaign(int id)
    {
        var campaign = await _context.Set<PromoCampaign>().FindAsync(id);
        if (campaign == null)
            return NotFound(new { error = "Campaña no encontrada" });

        _context.Set<PromoCampaign>().Remove(campaign);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Campaña eliminada", id });
    }

    [HttpPost("{id}/send")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<SendResultDto>> SendCampaign(int id, [FromBody] SendCampaignRequest request)
    {
        var campaign = await _context.Set<PromoCampaign>()
            .Include(c => c.Products).ThenInclude(p => p.Product)
            .Include(c => c.Customers).ThenInclude(c => c.Customer)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (campaign == null)
            return NotFound(new { error = "Campaña no encontrada" });

        if (campaign.Status == "completed" || campaign.Status == "cancelled")
            return BadRequest(new { error = "La campaña ya fue enviada o cancelada" });

        var companyInfo = await _context.Set<CompanyInfo>().FirstOrDefaultAsync();
        var companyName = companyInfo?.Name ?? "";

        var selectedChannels = request.Channels ?? new List<string> { "email" };
        var result = new SendResultDto { Message = "", Sent = 0, Failed = 0, Errors = new List<string>() };

        foreach (var cust in campaign.Customers)
        {
            if (selectedChannels.Contains("email") && !string.IsNullOrEmpty(cust.ContactEmail))
            {
                try
                {
                    var log = new PromoCampaignLog
                    {
                        CampaignId = id,
                        CustomerId = cust.CustomerId,
                        Channel = "email",
                        Recipient = cust.ContactEmail,
                        Subject = campaign.Name,
                        Status = "sent",
                        SentAt = DateTime.UtcNow,
                    };
                    _context.Set<PromoCampaignLog>().Add(log);
                    result.Sent++;
                }
                catch (Exception ex)
                {
                    _context.Set<PromoCampaignLog>().Add(new PromoCampaignLog
                    {
                        CampaignId = id,
                        CustomerId = cust.CustomerId,
                        Channel = "email",
                        Recipient = cust.ContactEmail,
                        Status = "failed",
                        ErrorMessage = ex.Message,
                        SentAt = DateTime.UtcNow,
                    });
                    result.Failed++;
                    result.Errors.Add($"Email to {cust.ContactEmail}: {ex.Message}");
                }
            }

            if (selectedChannels.Contains("whatsapp") && !string.IsNullOrEmpty(cust.ContactPhone))
            {
                try
                {
                    var log = new PromoCampaignLog
                    {
                        CampaignId = id,
                        CustomerId = cust.CustomerId,
                        Channel = "whatsapp",
                        Recipient = cust.ContactPhone,
                        Status = "sent",
                        SentAt = DateTime.UtcNow,
                    };
                    _context.Set<PromoCampaignLog>().Add(log);
                    result.Sent++;
                }
                catch (Exception ex)
                {
                    _context.Set<PromoCampaignLog>().Add(new PromoCampaignLog
                    {
                        CampaignId = id,
                        CustomerId = cust.CustomerId,
                        Channel = "whatsapp",
                        Recipient = cust.ContactPhone,
                        Status = "failed",
                        ErrorMessage = ex.Message,
                        SentAt = DateTime.UtcNow,
                    });
                    result.Failed++;
                    result.Errors.Add($"WhatsApp to {cust.ContactPhone}: {ex.Message}");
                }
            }
        }

        campaign.Status = "completed";
        campaign.SentAt = DateTime.UtcNow;
        campaign.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        result.Message = $"Campaña enviada: {result.Sent} enviados, {result.Failed} fallidos";
        return Ok(result);
    }

    [HttpGet("available-customers/list")]
    public async Task<ActionResult<List<AvailableCustomerDto>>> GetAvailableCustomers()
    {
        var customers = await _context.Set<Customer>()
            .Where(c => (c.Email != null && c.Email != "") || (c.Phone != null && c.Phone != ""))
            .OrderBy(c => c.FullName)
            .Select(c => new AvailableCustomerDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                Phone = c.Phone,
                DocumentNumber = c.DocumentNumber,
            })
            .ToListAsync();

        return Ok(customers);
    }

    [HttpGet("expiring-products/list")]
    public async Task<ActionResult<List<ExpiringProductDto>>> GetExpiringProducts([FromQuery] int? days)
    {
        var maxDays = days ?? 90;
        var cutoff = DateTime.UtcNow.AddDays(maxDays);

        var products = await _context.Set<Product>()
            .Include(p => p.Category)
            .Where(p => p.ExpiryDate != null && p.ExpiryDate <= cutoff && p.ExpiryDate >= DateTime.UtcNow && p.IsActive)
            .OrderBy(p => p.ExpiryDate)
            .Select(p => new ExpiringProductDto
            {
                Id = p.Id,
                Code = p.Code,
                Barcode = p.Barcode,
                Name = p.Name,
                CategoryName = p.Category != null ? p.Category.Name : null,
                SalePrice = p.SalePrice,
                PurchasePrice = p.PurchasePrice,
                ExpiryDate = p.ExpiryDate,
            })
            .ToListAsync();

        return Ok(products);
    }
}
