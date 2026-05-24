using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.API.Models;

[Table("CompanyInfo")]
public class CompanyInfo
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(200)]
    public string? BusinessName { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(500)]
    public string? LogoUrl { get; set; }

    [StringLength(20)]
    public string? TaxId { get; set; }

    [StringLength(300)]
    public string? ReceiptFooter { get; set; }

    [StringLength(100)]
    public string? Slogan { get; set; }

    [StringLength(5)]
    public string? CodigoPostal { get; set; }

    [StringLength(10)]
    public string? RegimenFiscalId { get; set; }

    [ForeignKey("RegimenFiscalId")]
    public CatRegimenFiscal? RegimenFiscal { get; set; }
}
