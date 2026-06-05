using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Categories")]
public class Category
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(100)]
	public string Name { get; set; }

	[StringLength(200)]
	public string? Description { get; set; }

	public bool IsActive { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public System.Collections.Generic.ICollection<Product> Products { get; set; }
}
