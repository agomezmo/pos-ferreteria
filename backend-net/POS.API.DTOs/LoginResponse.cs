using System;
namespace POS.API.DTOs;

public class LoginResponse
{
	public string Token { get; set; }

	public string FullName { get; set; }

	public string Role { get; set; }

	public int UserId { get; set; }

	public string Username { get; set; }

	public System.DateTime ExpiresAt { get; set; }
}
