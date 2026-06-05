using System;
namespace POS.API.DTOs;

public class CustomerDTO
{
	public int Id { get; set; }

	public string DocumentType { get; set; }

	public string DocumentNumber { get; set; }

	public string FullName { get; set; }

	public string? Phone { get; set; }

	public string? Email { get; set; }

	public string? Address { get; set; }

	public decimal CreditLimit { get; set; }

	public decimal CurrentBalance { get; set; }

	public bool IsCreditCustomer { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
