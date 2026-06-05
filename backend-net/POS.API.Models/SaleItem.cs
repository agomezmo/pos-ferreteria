using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("SaleItems")]
public class SaleItem
{
	[Key]
	public int Id { get; set; }

	public int SaleId { get; set; }

	[ForeignKey("SaleId")]
	public Sale Sale { get; set; }

	public int ProductId { get; set; }

	[ForeignKey("ProductId")]
	public Product Product { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Quantity { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal UnitPrice { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Discount { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Subtotal { get; set; }
}
