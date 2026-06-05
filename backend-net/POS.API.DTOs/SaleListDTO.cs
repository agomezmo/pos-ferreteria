using System;
namespace POS.API.DTOs;

public class SaleListDTO
{
	public int Id { get; set; }

	public string ReceiptNumber { get; set; }

	public string UserName { get; set; }

	public string? CustomerName { get; set; }

	public decimal Total { get; set; }

	public string PaymentMethod { get; set; }

	public string PaymentStatus { get; set; }

	public string SaleType { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
