namespace POS.API.DTOs;

public class UpdateCustomerRequest
{
	public string DocumentType { get; set; }

	public string DocumentNumber { get; set; }

	public string FullName { get; set; }

	public string? Phone { get; set; }

	public string? Email { get; set; }

	public string? Address { get; set; }

	public decimal CreditLimit { get; set; }

	public bool IsCreditCustomer { get; set; }
}
