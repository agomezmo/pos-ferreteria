using System;
namespace POS.API.DTOs;

public class CashRegisterDTO
{
	public int Id { get; set; }

	public string Name { get; set; }

	public string? Location { get; set; }

	public bool IsActive { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
