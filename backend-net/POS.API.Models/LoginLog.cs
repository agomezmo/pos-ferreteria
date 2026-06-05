using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("LoginLogs")]
public class LoginLog
{
	[Key]
	public int Id { get; set; }

	public int? UserId { get; set; }

	[ForeignKey("UserId")]
	public User? User { get; set; }

	[StringLength(50)]
	public string? IpAddress { get; set; }

	[Required]
	[StringLength(20)]
	public string Action { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
