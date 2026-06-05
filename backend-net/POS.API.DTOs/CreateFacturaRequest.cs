namespace POS.API.DTOs;

public class CreateFacturaRequest
{
	public int SaleId { get; set; }

	public int? CustomerId { get; set; }
}
