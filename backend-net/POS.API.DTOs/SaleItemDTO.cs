namespace POS.API.DTOs;

public class SaleItemDTO
{
	public int Id { get; set; }

	public int ProductId { get; set; }

	public string ProductCode { get; set; }

	public string ProductName { get; set; }

	public decimal Quantity { get; set; }

	public string Unit { get; set; }

	public decimal UnitPrice { get; set; }

	public decimal Discount { get; set; }

	public decimal Subtotal { get; set; }
}
