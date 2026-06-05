namespace POS.API.DTOs;

public class TopProductDTO
{
	public int ProductId { get; set; }

	public string ProductName { get; set; }

	public string ProductCode { get; set; }

	public string CategoryName { get; set; }

	public int TotalQuantity { get; set; }

	public decimal TotalRevenue { get; set; }
}
