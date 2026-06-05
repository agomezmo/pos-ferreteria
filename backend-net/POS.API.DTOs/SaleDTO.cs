using System;
using System.Collections.Generic;
namespace POS.API.DTOs;

public class SaleDTO
{
	public int Id { get; set; }

	public string ReceiptNumber { get; set; }

	public int UserId { get; set; }

	public string UserName { get; set; }

	public int? CustomerId { get; set; }

	public string? CustomerName { get; set; }

	public string? CustomerDocument { get; set; }

	public decimal Subtotal { get; set; }

	public decimal Tax { get; set; }

	public decimal Discount { get; set; }

	public decimal Total { get; set; }

	public string PaymentMethod { get; set; }

	public string PaymentStatus { get; set; }

	public string SaleType { get; set; }

	public string? Notes { get; set; }

	public int? CashRegisterSessionId { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public List<SaleItemDTO> Items { get; set; }
}
