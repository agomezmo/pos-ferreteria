namespace POS.API.DTOs;

public class CreateExpenseRequest
{
	public string Category { get; set; }

	public string Description { get; set; }

	public decimal Amount { get; set; }

	public string? PaymentMethod { get; set; }

	public string? Reference { get; set; }
}
