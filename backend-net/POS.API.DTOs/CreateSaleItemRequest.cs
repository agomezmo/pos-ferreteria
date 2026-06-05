namespace POS.API.DTOs;

public class CreateSaleItemRequest
{
	public int ProductId { get; set; }

	public decimal Quantity { get; set; }

	public decimal UnitPrice { get; set; }

	public decimal Discount { get; set; }
}
