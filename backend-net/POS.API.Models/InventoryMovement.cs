using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("InventoryMovements")]
public class InventoryMovement
{
	[Key]
	public int Id { get; set; }

	public int ProductId { get; set; }

	[ForeignKey("ProductId")]
	public Product Product { get; set; }

	[Required]
	[StringLength(10)]
	public string Type { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Quantity { get; set; }

	[StringLength(50)]
	public string? ReferenceType { get; set; }

	public int? ReferenceId { get; set; }

	[StringLength(500)]
	public string? Notes { get; set; }

	public int? UserId { get; set; }

	[ForeignKey("UserId")]
	public User? User { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
