using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Suppliers")]
public class Supplier
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(100)]
	public string Name { get; set; }

	[StringLength(100)]
	public string? ContactName { get; set; }

	[StringLength(20)]
	public string? Phone { get; set; }

	[StringLength(100)]
	public string? Email { get; set; }

	[StringLength(200)]
	public string? Address { get; set; }

	[StringLength(20)]
	public string? Rfc { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public System.Collections.Generic.ICollection<Product> Products { get; set; }
}
