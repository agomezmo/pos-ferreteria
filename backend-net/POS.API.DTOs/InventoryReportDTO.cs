using System.Collections.Generic;
namespace POS.API.DTOs;

public class InventoryReportDTO
{
	public int TotalProducts { get; set; }

	public int LowStockProducts { get; set; }

	public int OutOfStockProducts { get; set; }

	public decimal TotalInventoryValue { get; set; }

	public List<ProductDTO> LowStockItems { get; set; }
}
