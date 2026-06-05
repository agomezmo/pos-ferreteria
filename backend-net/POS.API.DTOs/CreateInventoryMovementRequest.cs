namespace POS.API.DTOs;

public class CreateInventoryMovementRequest
{
	public int ProductId { get; set; }

	public string Type { get; set; }

	public decimal Quantity { get; set; }

	public string? ReferenceType { get; set; }

	public int? ReferenceId { get; set; }

	public string? Notes { get; set; }
}
