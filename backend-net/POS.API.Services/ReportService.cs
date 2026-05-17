using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using POS.API.DTOs;
using POS.API.Data;

namespace POS.API.Services;

public class ReportService
{
	private readonly AppDbContext _context;

	public ReportService(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetDailyReportAsync_003Ed__2))]
	public System.Threading.Tasks.Task<DailyReportDTO> GetDailyReportAsync(System.DateTime date)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetDailyReportAsync_003Ed__2 _003CGetDailyReportAsync_003Ed__ = default(_003CGetDailyReportAsync_003Ed__2);
		_003CGetDailyReportAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<DailyReportDTO>.Create();
		_003CGetDailyReportAsync_003Ed__._003C_003E4__this = this;
		_003CGetDailyReportAsync_003Ed__.date = date;
		_003CGetDailyReportAsync_003Ed__._003C_003E1__state = -1;
		_003CGetDailyReportAsync_003Ed__._003C_003Et__builder.Start<_003CGetDailyReportAsync_003Ed__2>(ref _003CGetDailyReportAsync_003Ed__);
		return _003CGetDailyReportAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetTopProductsAsync_003Ed__3))]
	public System.Threading.Tasks.Task<List<TopProductDTO>> GetTopProductsAsync(System.DateTime startDate, System.DateTime endDate, int top = 10)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetTopProductsAsync_003Ed__3 _003CGetTopProductsAsync_003Ed__ = default(_003CGetTopProductsAsync_003Ed__3);
		_003CGetTopProductsAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<TopProductDTO>>.Create();
		_003CGetTopProductsAsync_003Ed__._003C_003E4__this = this;
		_003CGetTopProductsAsync_003Ed__.startDate = startDate;
		_003CGetTopProductsAsync_003Ed__.endDate = endDate;
		_003CGetTopProductsAsync_003Ed__.top = top;
		_003CGetTopProductsAsync_003Ed__._003C_003E1__state = -1;
		_003CGetTopProductsAsync_003Ed__._003C_003Et__builder.Start<_003CGetTopProductsAsync_003Ed__3>(ref _003CGetTopProductsAsync_003Ed__);
		return _003CGetTopProductsAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetInventoryReportAsync_003Ed__4))]
	public System.Threading.Tasks.Task<InventoryReportDTO> GetInventoryReportAsync()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetInventoryReportAsync_003Ed__4 _003CGetInventoryReportAsync_003Ed__ = default(_003CGetInventoryReportAsync_003Ed__4);
		_003CGetInventoryReportAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<InventoryReportDTO>.Create();
		_003CGetInventoryReportAsync_003Ed__._003C_003E4__this = this;
		_003CGetInventoryReportAsync_003Ed__._003C_003E1__state = -1;
		_003CGetInventoryReportAsync_003Ed__._003C_003Et__builder.Start<_003CGetInventoryReportAsync_003Ed__4>(ref _003CGetInventoryReportAsync_003Ed__);
		return _003CGetInventoryReportAsync_003Ed__._003C_003Et__builder.get_Task();
	}
}
