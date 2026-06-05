using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Alerts")]
public class Alert
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(30)]
	public string Type { get; set; }

	[Required]
	[StringLength(200)]
	public string Title { get; set; }

	[StringLength(500)]
	public string? Message { get; set; }

	[StringLength(50)]
	public string? ReferenceType { get; set; }

	public int? ReferenceId { get; set; }

	public bool IsRead { get; set; }

	public int? UserId { get; set; }

	[ForeignKey("UserId")]
	public User? User { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
