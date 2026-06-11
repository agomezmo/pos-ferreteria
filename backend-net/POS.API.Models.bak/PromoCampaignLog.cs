using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.API.Models;

[Table("promo_campaign_log")]
public class PromoCampaignLog
{
    [Key]
    public int Id { get; set; }

    public int CampaignId { get; set; }

    [ForeignKey("CampaignId")]
    public PromoCampaign Campaign { get; set; } = null!;

    public int? CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }

    [Required]
    [StringLength(20)]
    public string Channel { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Recipient { get; set; }

    [StringLength(500)]
    public string? Subject { get; set; }

    public string? Message { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "pending";

    [StringLength(500)]
    public string? ErrorMessage { get; set; }

    public DateTime? SentAt { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
