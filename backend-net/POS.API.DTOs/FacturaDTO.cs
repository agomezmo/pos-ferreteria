using System;
namespace POS.API.DTOs;

public class FacturaDTO
{
	public int Id { get; set; }

	public string Uuid { get; set; }

	public string Serie { get; set; }

	public string Folio { get; set; }

	public int SaleId { get; set; }

	public string? CustomerName { get; set; }

	public decimal Subtotal { get; set; }

	public decimal Iva { get; set; }

	public decimal Total { get; set; }

	public string Status { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
