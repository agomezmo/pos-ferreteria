using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.API.Models;

[Table("promo_campaign_products")]
public class PromoCampaignProduct
{
    [Key]
    public int Id { get; set; }

    public int CampaignId { get; set; }

    [ForeignKey("CampaignId")]
    public PromoCampaign Campaign { get; set; } = null!;

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal OfferPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal OriginalPrice { get; set; }

    public DateTime? ExpiryDate { get; set; }
}
