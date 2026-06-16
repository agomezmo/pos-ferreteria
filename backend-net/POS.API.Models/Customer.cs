using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Customers")]
public class Customer
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(20)]
	public string DocumentType { get; set; }

	[Required]
	[StringLength(20)]
	public string DocumentNumber { get; set; }

	[Required]
	[StringLength(100)]
	public string FullName { get; set; }

	[StringLength(20)]
	public string? Phone { get; set; }

	[StringLength(100)]
	public string? Email { get; set; }

	[StringLength(200)]
	public string? Address { get; set; }

	[Column("credit_limit", TypeName = "decimal(18,2)")]
	public decimal CreditLimit { get; set; }

	[Column("current_balance", TypeName = "decimal(18,2)")]
	public decimal CurrentBalance { get; set; }

	[Column("is_credit_customer")]
	public bool IsCreditCustomer { get; set; }

	public bool IsActive { get; set; }

	[StringLength(10)]
	public string? RegimenFiscalId { get; set; }

	[ForeignKey("RegimenFiscalId")]
	public CatRegimenFiscal? RegimenFiscal { get; set; }

	[StringLength(10)]
	public string? UsoCfdiId { get; set; }

	[ForeignKey("UsoCfdiId")]
	public CatUsoCfdi? UsoCfdi { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public System.DateTime? UpdatedAt { get; set; }

	public System.Collections.Generic.ICollection<Sale> Sales { get; set; }
}
