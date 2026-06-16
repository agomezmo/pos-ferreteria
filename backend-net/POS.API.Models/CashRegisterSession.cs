using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("CashRegisterSessions")]
public class CashRegisterSession
{
	[Key]
	public int Id { get; set; }

	public int CashRegisterId { get; set; }

	[ForeignKey("CashRegisterId")]
	public CashRegister CashRegister { get; set; }

	public int UserId { get; set; }

	[ForeignKey("UserId")]
	public User? User { get; set; }

	public bool IsActive { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal OpeningAmount { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal? ClosingAmount { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal ExpectedAmount { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal Difference { get; set; }

	[StringLength(200)]
	public string? OpeningNotes { get; set; }

	[StringLength(200)]
	public string? ClosingNotes { get; set; }

	public System.DateTime OpenedAt { get; set; }

	public System.DateTime? ClosedAt { get; set; }

	[Required]
	[StringLength(20)]
	public string Status { get; set; }

	public System.Collections.Generic.ICollection<Sale> Sales { get; set; }
}
