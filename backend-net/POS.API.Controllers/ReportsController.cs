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
public class ReportsController : ControllerBase
{
	private readonly ReportService _reportService;

	public ReportsController(ReportService reportService)
	{
		_reportService = reportService;
	}

	[AsyncStateMachine(typeof(_003CGetDailyReport_003Ed__2))]
	[HttpGet("daily")]
	public System.Threading.Tasks.Task<ActionResult<DailyReportDTO>> GetDailyReport([FromQuery] System.DateTime? date)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetDailyReport_003Ed__2 _003CGetDailyReport_003Ed__ = default(_003CGetDailyReport_003Ed__2);
		_003CGetDailyReport_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<DailyReportDTO>>.Create();
		_003CGetDailyReport_003Ed__._003C_003E4__this = this;
		_003CGetDailyReport_003Ed__.date = date;
		_003CGetDailyReport_003Ed__._003C_003E1__state = -1;
		_003CGetDailyReport_003Ed__._003C_003Et__builder.Start<_003CGetDailyReport_003Ed__2>(ref _003CGetDailyReport_003Ed__);
		return _003CGetDailyReport_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetTopProducts_003Ed__3))]
	[HttpGet("top-products")]
	public System.Threading.Tasks.Task<ActionResult<List<TopProductDTO>>> GetTopProducts([FromQuery] System.DateTime? startDate, [FromQuery] System.DateTime? endDate, [FromQuery] int top = 10)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetTopProducts_003Ed__3 _003CGetTopProducts_003Ed__ = default(_003CGetTopProducts_003Ed__3);
		_003CGetTopProducts_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<List<TopProductDTO>>>.Create();
		_003CGetTopProducts_003Ed__._003C_003E4__this = this;
		_003CGetTopProducts_003Ed__.startDate = startDate;
		_003CGetTopProducts_003Ed__.endDate = endDate;
		_003CGetTopProducts_003Ed__.top = top;
		_003CGetTopProducts_003Ed__._003C_003E1__state = -1;
		_003CGetTopProducts_003Ed__._003C_003Et__builder.Start<_003CGetTopProducts_003Ed__3>(ref _003CGetTopProducts_003Ed__);
		return _003CGetTopProducts_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetInventoryReport_003Ed__4))]
	[HttpGet("inventory")]
	public System.Threading.Tasks.Task<ActionResult<InventoryReportDTO>> GetInventoryReport()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetInventoryReport_003Ed__4 _003CGetInventoryReport_003Ed__ = default(_003CGetInventoryReport_003Ed__4);
		_003CGetInventoryReport_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<ActionResult<InventoryReportDTO>>.Create();
		_003CGetInventoryReport_003Ed__._003C_003E4__this = this;
		_003CGetInventoryReport_003Ed__._003C_003E1__state = -1;
		_003CGetInventoryReport_003Ed__._003C_003Et__builder.Start<_003CGetInventoryReport_003Ed__4>(ref _003CGetInventoryReport_003Ed__);
		return _003CGetInventoryReport_003Ed__._003C_003Et__builder.get_Task();
	}
}
