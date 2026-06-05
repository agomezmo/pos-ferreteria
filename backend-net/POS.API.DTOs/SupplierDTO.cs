using System;
namespace POS.API.DTOs;

public class SupplierDTO
{
	public int Id { get; set; }

	public string Name { get; set; }

	public string? ContactName { get; set; }

	public string? Phone { get; set; }

	public string? Email { get; set; }

	public string? Address { get; set; }

	public string? Rfc { get; set; }

	public int ProductCount { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
