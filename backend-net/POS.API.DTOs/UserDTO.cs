using System;
namespace POS.API.DTOs;

public class UserDTO
{
	public int Id { get; set; }

	public string Username { get; set; }

	public string FullName { get; set; }

	public string? Email { get; set; }

	public string RoleName { get; set; }

	public int RoleId { get; set; }

	public bool IsActive { get; set; }

	public System.DateTime? LastLogin { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
