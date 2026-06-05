namespace POS.API.DTOs;

public class OpenSessionRequest
{
	public int CashRegisterId { get; set; }

	public decimal OpeningAmount { get; set; }

	public string? OpeningNotes { get; set; }
}
