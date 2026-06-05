using System.Collections.Generic;
namespace POS.API.DTOs;

public class CreateSaleRequest
{
	public int? CustomerId { get; set; }

	public decimal Discount { get; set; }

	public string PaymentMethod { get; set; }

	public string SaleType { get; set; }

	public string? Notes { get; set; }

	public int? CashRegisterSessionId { get; set; }

	public List<CreateSaleItemRequest> Items { get; set; }
}
