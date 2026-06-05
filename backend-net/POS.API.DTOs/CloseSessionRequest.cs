namespace POS.API.DTOs;

public class CloseSessionRequest
{
	public decimal ClosingAmount { get; set; }

	public string? ClosingNotes { get; set; }
}
