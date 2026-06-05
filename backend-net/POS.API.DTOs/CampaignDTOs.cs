using System;
using System.Collections.Generic;

namespace POS.API.DTOs;

public class CampaignDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = "draft";
    public string OfferType { get; set; } = "cost_price";
    public decimal? OfferValue { get; set; }
    public int? MinExpiryDays { get; set; }
    public int? MaxExpiryDays { get; set; }
    public string? Notes { get; set; }
    public string? CreatedByName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
    public int ProductCount { get; set; }
    public int CustomerCount { get; set; }
    public int SentCount { get; set; }
}

public class CampaignDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = "draft";
    public string OfferType { get; set; } = "cost_price";
    public decimal? OfferValue { get; set; }
    public int? MinExpiryDays { get; set; }
    public int? MaxExpiryDays { get; set; }
    public string? Notes { get; set; }
    public string? CreatedByName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
    public List<CampaignProductDto> Products { get; set; } = new();
    public List<CampaignCustomerDto> Customers { get; set; } = new();
    public List<CampaignLogDto> Logs { get; set; } = new();
}

public class CampaignProductDto
{
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductCode { get; set; }
    public string? Barcode { get; set; }
    public string? CategoryName { get; set; }
    public decimal OfferPrice { get; set; }
    public decimal OriginalPrice { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

public class CampaignCustomerDto
{
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? DocumentNumber { get; set; }
}

public class CampaignLogDto
{
    public int Id { get; set; }
    public int CampaignId { get; set; }
    public int? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string Channel { get; set; } = string.Empty;
    public string? Recipient { get; set; }
    public string? Subject { get; set; }
    public string Status { get; set; } = "pending";
    public string? ErrorMessage { get; set; }
    public DateTime? SentAt { get; set; }
}

public class CreateCampaignRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string OfferType { get; set; } = "cost_price";
    public decimal? OfferValue { get; set; }
    public int MinExpiryDays { get; set; } = 30;
    public int MaxExpiryDays { get; set; } = 90;
    public string? Notes { get; set; }
    public List<int> ProductIds { get; set; } = new();
    public List<int> CustomerIds { get; set; } = new();
}

public class UpdateCampaignRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public string? OfferType { get; set; }
    public decimal? OfferValue { get; set; }
    public string? Notes { get; set; }
}

public class SendCampaignRequest
{
    public List<string> Channels { get; set; } = new() { "email" };
}

public class AvailableCustomerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? DocumentNumber { get; set; }
}

public class ExpiringProductDto
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Barcode { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? CategoryName { get; set; }
    public decimal SalePrice { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

public class SendResultDto
{
    public string Message { get; set; } = string.Empty;
    public int Sent { get; set; }
    public int Failed { get; set; }
    public List<string> Errors { get; set; } = new();
}
