using System;
namespace POS.API.DTOs;

public class InventoryMovementDTO
{
	public int Id { get; set; }

	public int ProductId { get; set; }

	public string ProductName { get; set; }

	public string ProductCode { get; set; }

	public string Type { get; set; }

	public decimal Quantity { get; set; }

	public string? ReferenceType { get; set; }

	public int? ReferenceId { get; set; }

	public string? Notes { get; set; }

	public string? UserName { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
