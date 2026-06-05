using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("TaxRates")]
public class TaxRate
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(100)]
	public string Name { get; set; }

	[Column(TypeName = "decimal(5,2)")]
	public decimal Rate { get; set; }

	public bool IsActive { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
