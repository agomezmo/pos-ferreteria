using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Sales")]
public class Sale
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(20)]
	public string ReceiptNumber { get; set; }

	public int UserId { get; set; }

	[ForeignKey("UserId")]
	public User User { get; set; }

	public int? CustomerId { get; set; }

	[ForeignKey("CustomerId")]
	public Customer? Customer { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Subtotal { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Tax { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Discount { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Total { get; set; }

	[Required]
	[StringLength(30)]
	public string PaymentMethod { get; set; }

	[Required]
	[StringLength(20)]
	public string PaymentStatus { get; set; }

	[Required]
	[StringLength(20)]
	public string Status { get; set; }

	[Column("sale_type")]
	[StringLength(20)]
	public string SaleType { get; set; }

	[StringLength(500)]
	public string? Notes { get; set; }

	public int? CashRegisterSessionId { get; set; }

	[ForeignKey("CashRegisterSessionId")]
	public CashRegisterSession? CashRegisterSession { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public System.Collections.Generic.ICollection<SaleItem> SaleItems { get; set; }

	public System.Collections.Generic.ICollection<Payment> Payments { get; set; }

	public System.Collections.Generic.ICollection<Return> Returns { get; set; }
}
