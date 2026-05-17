using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.DTOs;
using POS.API.Services;

namespace POS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalesController : ControllerBase
{
	private readonly SaleService _saleService;

	public SalesController(SaleService saleService)
	{
		_saleService = saleService;
	}

	[AsyncStateMachine(typeof(_003CGetSales_003Ed__2))]
	[HttpGet]
	public System.Threading.Tasks.Task<ActionResult<List<SaleListDTO>>> GetSales([FromQuery] System.DateTime? startDate, [FromQuery] System.DateTime? endDate)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetSales_003Ed__2 _003CGetSales_003Ed__ = default(_003CGetSales_003Ed__2);
		_003CGetSales_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<SaleListDTO>>>.Create();
		_003CGetSales_003Ed__._003C_003E4__this = this;
		_003CGetSales_003Ed__.startDate = startDate;
		_003CGetSales_003Ed__.endDate = endDate;
		_003CGetSales_003Ed__._003C_003E1__state = -1;
		_003CGetSales_003Ed__._003C_003Et__builder.Start<_003CGetSales_003Ed__2>(ref _003CGetSales_003Ed__);
		return _003CGetSales_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetSale_003Ed__3))]
	[HttpGet("{id}")]
	public System.Threading.Tasks.Task<ActionResult<SaleDTO>> GetSale(int id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetSale_003Ed__3 _003CGetSale_003Ed__ = default(_003CGetSale_003Ed__3);
		_003CGetSale_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<SaleDTO>>.Create();
		_003CGetSale_003Ed__._003C_003E4__this = this;
		_003CGetSale_003Ed__.id = id;
		_003CGetSale_003Ed__._003C_003E1__state = -1;
		_003CGetSale_003Ed__._003C_003Et__builder.Start<_003CGetSale_003Ed__3>(ref _003CGetSale_003Ed__);
		return _003CGetSale_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateSale_003Ed__4))]
	[HttpPost]
	public System.Threading.Tasks.Task<ActionResult<SaleDTO>> CreateSale([FromBody] CreateSaleRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateSale_003Ed__4 _003CCreateSale_003Ed__ = default(_003CCreateSale_003Ed__4);
		_003CCreateSale_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<SaleDTO>>.Create();
		_003CCreateSale_003Ed__._003C_003E4__this = this;
		_003CCreateSale_003Ed__.request = request;
		_003CCreateSale_003Ed__._003C_003E1__state = -1;
		_003CCreateSale_003Ed__._003C_003Et__builder.Start<_003CCreateSale_003Ed__4>(ref _003CCreateSale_003Ed__);
		return _003CCreateSale_003Ed__._003C_003Et__builder.get_Task();
	}
}
