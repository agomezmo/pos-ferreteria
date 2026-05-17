using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using POS.API.DTOs;
using POS.API.Data;

namespace POS.API.Services;

public class CashRegisterService
{
	private readonly AppDbContext _context;

	public CashRegisterService(AppDbContext context)
	{
		_context = context;
	}

	[AsyncStateMachine(typeof(_003CGetCashRegistersAsync_003Ed__2))]
	public System.Threading.Tasks.Task<List<CashRegisterDTO>> GetCashRegistersAsync()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetCashRegistersAsync_003Ed__2 _003CGetCashRegistersAsync_003Ed__ = default(_003CGetCashRegistersAsync_003Ed__2);
		_003CGetCashRegistersAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<CashRegisterDTO>>.Create();
		_003CGetCashRegistersAsync_003Ed__._003C_003E4__this = this;
		_003CGetCashRegistersAsync_003Ed__._003C_003E1__state = -1;
		_003CGetCashRegistersAsync_003Ed__._003C_003Et__builder.Start<_003CGetCashRegistersAsync_003Ed__2>(ref _003CGetCashRegistersAsync_003Ed__);
		return _003CGetCashRegistersAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCreateCashRegisterAsync_003Ed__3))]
	public System.Threading.Tasks.Task<CashRegisterDTO> CreateCashRegisterAsync(CreateCashRegisterRequest request)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCreateCashRegisterAsync_003Ed__3 _003CCreateCashRegisterAsync_003Ed__ = default(_003CCreateCashRegisterAsync_003Ed__3);
		_003CCreateCashRegisterAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<CashRegisterDTO>.Create();
		_003CCreateCashRegisterAsync_003Ed__._003C_003E4__this = this;
		_003CCreateCashRegisterAsync_003Ed__.request = request;
		_003CCreateCashRegisterAsync_003Ed__._003C_003E1__state = -1;
		_003CCreateCashRegisterAsync_003Ed__._003C_003Et__builder.Start<_003CCreateCashRegisterAsync_003Ed__3>(ref _003CCreateCashRegisterAsync_003Ed__);
		return _003CCreateCashRegisterAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003COpenSessionAsync_003Ed__4))]
	public System.Threading.Tasks.Task<CashRegisterSessionDTO?> OpenSessionAsync(OpenSessionRequest request, int userId)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003COpenSessionAsync_003Ed__4 _003COpenSessionAsync_003Ed__ = default(_003COpenSessionAsync_003Ed__4);
		_003COpenSessionAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<CashRegisterSessionDTO>.Create();
		_003COpenSessionAsync_003Ed__._003C_003E4__this = this;
		_003COpenSessionAsync_003Ed__.request = request;
		_003COpenSessionAsync_003Ed__.userId = userId;
		_003COpenSessionAsync_003Ed__._003C_003E1__state = -1;
		_003COpenSessionAsync_003Ed__._003C_003Et__builder.Start<_003COpenSessionAsync_003Ed__4>(ref _003COpenSessionAsync_003Ed__);
		return _003COpenSessionAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CCloseSessionAsync_003Ed__5))]
	public System.Threading.Tasks.Task<CashRegisterSessionDTO?> CloseSessionAsync(int sessionId, CloseSessionRequest request, int userId)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CCloseSessionAsync_003Ed__5 _003CCloseSessionAsync_003Ed__ = default(_003CCloseSessionAsync_003Ed__5);
		_003CCloseSessionAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<CashRegisterSessionDTO>.Create();
		_003CCloseSessionAsync_003Ed__._003C_003E4__this = this;
		_003CCloseSessionAsync_003Ed__.sessionId = sessionId;
		_003CCloseSessionAsync_003Ed__.request = request;
		_003CCloseSessionAsync_003Ed__._003C_003E1__state = -1;
		_003CCloseSessionAsync_003Ed__._003C_003Et__builder.Start<_003CCloseSessionAsync_003Ed__5>(ref _003CCloseSessionAsync_003Ed__);
		return _003CCloseSessionAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetCurrentSessionAsync_003Ed__6))]
	public System.Threading.Tasks.Task<CashRegisterSessionDTO?> GetCurrentSessionAsync(int cashRegisterId)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetCurrentSessionAsync_003Ed__6 _003CGetCurrentSessionAsync_003Ed__ = default(_003CGetCurrentSessionAsync_003Ed__6);
		_003CGetCurrentSessionAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<CashRegisterSessionDTO>.Create();
		_003CGetCurrentSessionAsync_003Ed__._003C_003E4__this = this;
		_003CGetCurrentSessionAsync_003Ed__.cashRegisterId = cashRegisterId;
		_003CGetCurrentSessionAsync_003Ed__._003C_003E1__state = -1;
		_003CGetCurrentSessionAsync_003Ed__._003C_003Et__builder.Start<_003CGetCurrentSessionAsync_003Ed__6>(ref _003CGetCurrentSessionAsync_003Ed__);
		return _003CGetCurrentSessionAsync_003Ed__._003C_003Et__builder.get_Task();
	}

	[AsyncStateMachine(typeof(_003CGetSessionsAsync_003Ed__7))]
	public System.Threading.Tasks.Task<List<CashRegisterSessionDTO>> GetSessionsAsync(System.DateTime? startDate = null, System.DateTime? endDate = null)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_003CGetSessionsAsync_003Ed__7 _003CGetSessionsAsync_003Ed__ = default(_003CGetSessionsAsync_003Ed__7);
		_003CGetSessionsAsync_003Ed__._003C_003Et__builder = AsyncTaskMethodBuilder<List<CashRegisterSessionDTO>>.Create();
		_003CGetSessionsAsync_003Ed__._003C_003E4__this = this;
		_003CGetSessionsAsync_003Ed__.startDate = startDate;
		_003CGetSessionsAsync_003Ed__.endDate = endDate;
		_003CGetSessionsAsync_003Ed__._003C_003E1__state = -1;
		_003CGetSessionsAsync_003Ed__._003C_003Et__builder.Start<_003CGetSessionsAsync_003Ed__7>(ref _003CGetSessionsAsync_003Ed__);
		return _003CGetSessionsAsync_003Ed__._003C_003Et__builder.get_Task();
	}
}
