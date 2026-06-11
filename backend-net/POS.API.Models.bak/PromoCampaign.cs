using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.API.Models;

[Table("promo_campaigns")]
public class PromoCampaign
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "draft";

    [Required]
    [StringLength(20)]
    public string OfferType { get; set; } = "cost_price";

    [Column(TypeName = "decimal(18,2)")]
    public decimal? OfferValue { get; set; }

    public int? MinExpiryDays { get; set; } = 30;

    public int? MaxExpiryDays { get; set; } = 90;

    [StringLength(500)]
    public string? Notes { get; set; }

    public int? CreatedBy { get; set; }

    [ForeignKey("CreatedBy")]
    public User? CreatedByUser { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? SentAt { get; set; }

    public ICollection<PromoCampaignProduct> Products { get; set; } = new List<PromoCampaignProduct>();
    public ICollection<PromoCampaignCustomer> Customers { get; set; } = new List<PromoCampaignCustomer>();
    public ICollection<PromoCampaignLog> Logs { get; set; } = new List<PromoCampaignLog>();
}
