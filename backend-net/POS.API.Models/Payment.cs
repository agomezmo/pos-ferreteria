using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Payments")]
public class Payment
{
	[Key]
	public int Id { get; set; }

	public int SaleId { get; set; }

	[ForeignKey("SaleId")]
	public Sale Sale { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Amount { get; set; }

	[Required]
	[StringLength(30)]
	public string PaymentMethod { get; set; }

	[StringLength(100)]
	public string? Reference { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
