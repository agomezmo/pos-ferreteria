using System;
using System.Collections.Generic;
namespace POS.API.DTOs;

public class ReturnDTO
{
	public int Id { get; set; }

	public int SaleId { get; set; }

	public string ReceiptNumber { get; set; }

	public string? UserName { get; set; }

	public string Reason { get; set; }

	public decimal Total { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public List<ReturnItemDTO> Items { get; set; }
}
