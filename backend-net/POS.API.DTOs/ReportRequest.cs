using System;
namespace POS.API.DTOs;

public class ReportRequest
{
	public System.DateTime StartDate { get; set; }

	public System.DateTime EndDate { get; set; }
}
