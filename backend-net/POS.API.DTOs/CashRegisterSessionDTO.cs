using System;
namespace POS.API.DTOs;

public class CashRegisterSessionDTO
{
	public int Id { get; set; }

	public int CashRegisterId { get; set; }

	public string CashRegisterName { get; set; }

	public int UserId { get; set; }

	public string UserName { get; set; }

	public decimal OpeningAmount { get; set; }

	public decimal? ClosingAmount { get; set; }

	public decimal ExpectedAmount { get; set; }

	public decimal Difference { get; set; }

	public string? OpeningNotes { get; set; }

	public string? ClosingNotes { get; set; }

	public System.DateTime OpenedAt { get; set; }

	public System.DateTime? ClosedAt { get; set; }

	public string Status { get; set; }
}
