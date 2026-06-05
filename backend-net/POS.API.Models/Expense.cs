using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Expenses")]
public class Expense
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(100)]
	public string Category { get; set; }

	[Required]
	[StringLength(500)]
	public string Description { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Amount { get; set; }

	[StringLength(30)]
	public string? PaymentMethod { get; set; }

	[StringLength(100)]
	public string? Reference { get; set; }

	public int? UserId { get; set; }

	[ForeignKey("UserId")]
	public User? User { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
