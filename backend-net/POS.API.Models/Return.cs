using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Returns")]
public class Return
{
	[Key]
	public int Id { get; set; }

	public int SaleId { get; set; }

	[ForeignKey("SaleId")]
	public Sale Sale { get; set; }

	public int? UserId { get; set; }

	[ForeignKey("UserId")]
	public User? User { get; set; }

	[Required]
	[StringLength(500)]
	public string Reason { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Total { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public System.Collections.Generic.ICollection<ReturnItem> ReturnItems { get; set; }
}
