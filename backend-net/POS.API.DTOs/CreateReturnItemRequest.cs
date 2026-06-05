namespace POS.API.DTOs;

public class CreateReturnItemRequest
{
	public int ProductId { get; set; }

	public decimal Quantity { get; set; }

	public decimal UnitPrice { get; set; }
}
