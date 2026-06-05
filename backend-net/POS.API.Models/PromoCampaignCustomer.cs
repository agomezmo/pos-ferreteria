using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.API.Models;

[Table("promo_campaign_customers")]
public class PromoCampaignCustomer
{
    [Key]
    public int Id { get; set; }

    public int CampaignId { get; set; }

    [ForeignKey("CampaignId")]
    public PromoCampaign Campaign { get; set; } = null!;

    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; } = null!;

    [StringLength(100)]
    public string? ContactEmail { get; set; }

    [StringLength(20)]
    public string? ContactPhone { get; set; }
}
