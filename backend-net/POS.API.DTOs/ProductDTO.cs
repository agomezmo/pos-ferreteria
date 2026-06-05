using System;
namespace POS.API.DTOs;

public class ProductDTO
{
	public int Id { get; set; }

	public string Code { get; set; }

	public string? Barcode { get; set; }

	public string Name { get; set; }

	public string? Description { get; set; }

	public int CategoryId { get; set; }

	public string CategoryName { get; set; }

	public int? SupplierId { get; set; }

	public string? SupplierName { get; set; }

	public decimal PurchasePrice { get; set; }

	public decimal SalePrice { get; set; }

	public decimal WholesalePrice { get; set; }

	public decimal Stock { get; set; }

	public decimal MinStock { get; set; }

	public string Unit { get; set; }

	public bool IsActive { get; set; }

	public bool RequiresTax { get; set; }

	public bool IsService { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
