using System;
namespace POS.API.DTOs;

public class CategoryDTO
{
	public int Id { get; set; }

	public string Name { get; set; }

	public string? Description { get; set; }

	public bool IsActive { get; set; }

	public int ProductCount { get; set; }

	public System.DateTime CreatedAt { get; set; }
}
