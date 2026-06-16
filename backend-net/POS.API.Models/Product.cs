using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Products")]
public class Product
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(50)]
	public string Code { get; set; }

	[StringLength(50)]
	public string? Barcode { get; set; }

	[Required]
	[StringLength(200)]
	public string Name { get; set; }

	[StringLength(500)]
	public string? Description { get; set; }

	public int CategoryId { get; set; }

	[ForeignKey("CategoryId")]
	public Category Category { get; set; }

	public int? SupplierId { get; set; }

	[ForeignKey("SupplierId")]
	public Supplier? Supplier { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal PurchasePrice { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal SalePrice { get; set; }

	[Column("wholesale_price", TypeName = "decimal(18,2)")]
	public decimal WholesalePrice { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Stock { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal MinStock { get; set; }

	[StringLength(20)]
	public string Unit { get; set; }

	public bool IsActive { get; set; }

	public System.DateTime? ExpiryDate { get; set; }

	[Column("requires_tax")]
	public bool RequiresTax { get; set; }

	[Column("is_service")]
	public bool IsService { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public System.DateTime? UpdatedAt { get; set; }

	public System.Collections.Generic.ICollection<SaleItem> SaleItems { get; set; }

	public System.Collections.Generic.ICollection<InventoryMovement> InventoryMovements { get; set; }

	public System.Collections.Generic.ICollection<ReturnItem> ReturnItems { get; set; }
}
