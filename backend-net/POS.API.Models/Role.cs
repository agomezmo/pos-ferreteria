using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Roles")]
public class Role
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(50)]
	public string Name { get; set; }

	[StringLength(200)]
	public string? Description { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public System.Collections.Generic.ICollection<User> Users { get; set; }
}
