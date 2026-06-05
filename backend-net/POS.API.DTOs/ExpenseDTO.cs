using System;
namespace POS.API.DTOs;

public class ExpenseDTO
{
	public int Id { get; set; }

	public string Category { get; set; }

	public string Description { get; set; }

	public decimal Amount { get; set; }

	public string? PaymentMethod { get; set; }

	public string? Reference { get; set; }

	public string? UserName { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
