using System.Collections.Generic;
namespace POS.API.DTOs;

public class CreateReturnRequest
{
	public int SaleId { get; set; }

	public string Reason { get; set; }

	public List<CreateReturnItemRequest> Items { get; set; }
}
