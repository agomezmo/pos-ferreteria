namespace POS.API.DTOs;

public class UpdateCompanyInfoRequest
{
    public string Name { get; set; } = string.Empty;
    public string? BusinessName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? LogoUrl { get; set; }
    public string? TaxId { get; set; }
    public string? ReceiptFooter { get; set; }
    public string? Slogan { get; set; }
    public string? CodigoPostal { get; set; }
}
