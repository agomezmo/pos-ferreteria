using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("ReturnItems")]
public class ReturnItem
{
	[Key]
	public int Id { get; set; }

	public int ReturnId { get; set; }

	[ForeignKey("ReturnId")]
	public Return Return { get; set; }

	public int ProductId { get; set; }

	[ForeignKey("ProductId")]
	public Product Product { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Quantity { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal UnitPrice { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Subtotal { get; set; }
}
