using System;
namespace POS.API.DTOs;

public class DailyReportDTO
{
	public System.DateTime Date { get; set; }

	public int TotalSales { get; set; }

	public decimal TotalRevenue { get; set; }

	public decimal TotalCash { get; set; }

	public decimal TotalCard { get; set; }

	public decimal TotalTransfer { get; set; }

	public decimal TotalCredit { get; set; }

	public decimal TotalTax { get; set; }

	public decimal TotalDiscount { get; set; }

	public decimal TotalExpenses { get; set; }

	public int TotalProductsSold { get; set; }

	public decimal AverageTicket { get; set; }
}
