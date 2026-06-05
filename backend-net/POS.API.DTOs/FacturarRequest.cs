namespace POS.API.DTOs;

public class FacturarRequest
{
	public int SaleId { get; set; }

	public int CustomerId { get; set; }

	public string UsoCfdi { get; set; }

	public string FormaPago { get; set; }

	public string MetodoPago { get; set; }
}
