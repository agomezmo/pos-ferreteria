using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS.API.Models;

[Table("Users")]
public class User
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(50)]
	public string Username { get; set; }

	[Required]
	public string PasswordHash { get; set; }

	[StringLength(100)]
	public string? Email { get; set; }

	[Required]
	[StringLength(100)]
	public string FullName { get; set; }

	public int RoleId { get; set; }

	[ForeignKey("RoleId")]
	public Role Role { get; set; }

	public bool IsActive { get; set; }

	public System.DateTime? LastLogin { get; set; }

	public System.DateTime CreatedAt { get; set; }

	public System.DateTime? UpdatedAt { get; set; }

	public System.Collections.Generic.ICollection<Sale> Sales { get; set; }

	public System.Collections.Generic.ICollection<CashRegisterSession> CashRegisterSessions { get; set; }

	public System.Collections.Generic.ICollection<InventoryMovement> InventoryMovements { get; set; }

	public System.Collections.Generic.ICollection<LoginLog> LoginLogs { get; set; }

	public System.Collections.Generic.ICollection<Alert> Alerts { get; set; }

	public System.Collections.Generic.ICollection<Expense> Expenses { get; set; }
}
