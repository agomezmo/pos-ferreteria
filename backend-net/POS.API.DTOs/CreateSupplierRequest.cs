namespace POS.API.DTOs;

public class CreateSupplierRequest
{
	public string Name { get; set; }

	public string? ContactName { get; set; }

	public string? Phone { get; set; }

	public string? Email { get; set; }

	public string? Address { get; set; }

	public string? Rfc { get; set; }
}
